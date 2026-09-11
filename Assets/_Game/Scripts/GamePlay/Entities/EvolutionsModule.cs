using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Entities
{
public class EvolutionsModule : IStartable, IDisposable
{
    private readonly EvolutionsDatabase _evolutionsDatabase;
    private readonly RaritiesDatabase _raritiesDatabase;
    private readonly EntityScope _entity;
    private readonly ExperienceModule _experience;
    private readonly StatsContainer _statsContainer;
    private readonly AbilitiesModule _abilities;
    private readonly ExperienceFactory _experienceFactory;
    
    private int _minEvolutions = 3; // temporarly! it should be deleted due to shop feature in the future
    
    public List<Evolution> Evolutions { get; } = new();

    public event Action<List<Evolution>> OnSlotsFilled;
    public event Action<Evolution> OnEvolutionApplied;

    public EvolutionsModule(EvolutionsDatabase evolutionsDatabase, RaritiesDatabase raritiesDatabase,
        EntityScope entity, ExperienceModule experience, StatsContainer statsContainer, AbilitiesModule abilities, ExperienceFactory experienceFactory)
    {
        _evolutionsDatabase = evolutionsDatabase;
        _raritiesDatabase = raritiesDatabase;
        _entity = entity;
        _experience = experience;
        _statsContainer = statsContainer;
        _abilities = abilities;
        _experienceFactory = experienceFactory;
    }

    public void Start()
    {
        _experience.OnLevelChanged += OnLevelUpdated;
        foreach (var evolution in _evolutionsDatabase.GenerateEvolutions())
        {
            Evolutions.Add(evolution);
        }
    }
    
    private void OnLevelUpdated(int level)
    {
        if (Evolutions.Count(evolution => evolution.State == EvolutionState.IsAble) <= 0) return;
        
        FillSlots();
    }

    public void ChooseEvolution(Evolution evolution)
    {
        evolution.OnLevelUp += OnEvolutionLevelUp;
        evolution.Apply(_entity, _experienceFactory);
        _statsContainer.AddSource(evolution);
        _abilities.Add(evolution.Config.Abilities);

        UnlockEvolutions();
        BlockEvolutions(evolution);
        
        OnEvolutionApplied?.Invoke(evolution);
    }

    private void OnEvolutionLevelUp(Evolution evolution, int level)
    {
        foreach (var rarity in _raritiesDatabase.Rarities)
        {
            if (rarity.Index != level) continue;
            
            evolution.UpdateRarity(rarity);
            return;
        }
    }

    private void UnlockEvolutions()
    {
        foreach (var evolution in Evolutions)
        {
            if (evolution.State != EvolutionState.IsHidden) continue;
            
            var counter = 0;
            
            foreach (var requiredConfig in evolution.Config.Requires)
            {
                var requiredEvolution = Evolutions.FirstOrDefault(e => e.Config == requiredConfig);
                
                if (requiredEvolution != null && requiredEvolution.State == EvolutionState.IsActive)
                {
                    counter++;
                }
            }

            if (counter == evolution.Config.Requires.Length)
            {
                evolution.Unlock();
            }
        }
    }
    
    private void BlockEvolutions(Evolution evolution)
    {
        foreach (var evo in Evolutions.Where(evo => evolution.Config.Blocks.Contains(evo.Config)))
        {
            evo.Block();
        }
    }

    private void FillSlots()
    {
        var evolutions = GetRandomEvolutions(_minEvolutions);

        foreach (var evolution in evolutions)
        {
            evolution.SetRarity(_raritiesDatabase.GetRandom());
        }
        
        OnSlotsFilled?.Invoke(evolutions);
    }

    private List<Evolution> GetRandomEvolutions(int amount)
    {
        var availableEvolutions = Evolutions.Where(evolution => evolution.State == EvolutionState.IsAble).ToList();
        var slotsToFill = Mathf.Min(amount, availableEvolutions.Count);
    
        var evolutions = new List<Evolution>(slotsToFill);
    
        for (var i = 0; i < slotsToFill; i++)
        {
            var index = Random.Range(0, availableEvolutions.Count);
            evolutions.Add(availableEvolutions[index]);
            availableEvolutions.RemoveAt(index); 
        }

        return evolutions;
    }

    public void Dispose()
    {
        _experience.OnLevelChanged -= OnLevelUpdated;

        foreach (var evolution in Evolutions)
        {
            evolution.OnLevelUp -= OnEvolutionLevelUp;
            evolution.Dispose();
        }
    }
}
}