using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Entities
{
public class EvolutionsModule: IStartable, IDisposable
{
    [Inject] private EvolutionsDatabase _evolutionsDatabase;
    [Inject] private RaritiesDatabase _raritiesDatabase;
    [Inject] private EntityModel _entity;
    [Inject] private ExperienceModule _experience;
    [Inject] private EntityStats _stats;
    [Inject] private AbilitiesModule _abilities;
    private int _minEvolutions;
    
    
    private readonly List<Evolution> _evolutions = new();

    public event Action<List<Evolution>> OnSlotsFilled;
    public event Action<Evolution> OnEvolutionApplied;

    public void Start()
    {
        _minEvolutions = 3; // temporarly! it should be deleted due to shop feature in the future
        _experience.OnLevelChanged += OnLevelUpdated;
        foreach (var evolution in _evolutionsDatabase.GenerateEvolutions())
        {
            _evolutions.Add(evolution);
        }
    }
    
    private void OnLevelUpdated(int level)
    {
        if (_evolutions.Count(evolution => evolution.State == EvolutionState.IsAble) <= 0) return;
        
        FillSlots();
    }

    public void ChooseEvolution(Evolution evolution)
    {
        evolution.OnLevelUp += OnEvolutionLevelUp;
        evolution.Apply(_entity);
        _stats.AddSource(evolution);
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
        foreach (var evolution in _evolutions)
        {
            if (evolution.State != EvolutionState.IsHidden) continue;
            
            var counter = 0;
            
            foreach (var requiredConfig in evolution.Config.Requires)
            {
                var requiredEvolution = _evolutions.FirstOrDefault(e => e.Config == requiredConfig);
                
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
        foreach (var evo in _evolutions.Where(evo => evolution.Config.Blocks.Contains(evo.Config)))
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
        var availableEvolutions = _evolutions.Where(evolution => evolution.State == EvolutionState.IsAble).ToList();
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

        foreach (var evolution in _evolutions)
        {
            evolution.OnLevelUp -= OnEvolutionLevelUp;
            evolution.Dispose();
        }
    }
}
}