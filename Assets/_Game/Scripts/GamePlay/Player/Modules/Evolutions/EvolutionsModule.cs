using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules.Abilities;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Player.Modules.Evolutions
{
public class EvolutionsModule: IDisposable
{
    private EvolutionsDatabase _evolutionsDatabase;
    private RaritiesDatabase _raritiesDatabase;
    private int _minEvolutions;

    private ExperienceModule _experience;
    private MovementModule _movement;
    private EntityStats _stats;
    private AbilitiesModule _abilities;
    private PlayerModel _player;
    
    private readonly List<Evolution> _evolutions = new();

    public event Action<List<Evolution>> OnSlotsFilled;
    public event Action<Evolution> OnEvolutionApplied;

    public EvolutionsModule(ExperienceModule experience, MovementModule movement, EntityStats stats, AbilitiesModule abilities)
    {
        _experience = experience;
        _movement = movement;
        _stats = stats;
        _abilities = abilities;
        _experience.OnLevelChanged += OnLevelUpdated;
    }

    public void SetModel(PlayerModel model)
    {
        _player = model;
    }
    
    public void Initialize(EvolutionsDatabase evolutionsDatabase, RaritiesDatabase raritiesDatabase, int minEvolutions)
    {
        _evolutionsDatabase  = evolutionsDatabase;
        _raritiesDatabase = raritiesDatabase;
        _minEvolutions = minEvolutions;

        foreach (var evolution in _evolutionsDatabase.GenerateEvolutions())
        {
            _evolutions.Add(evolution);
            evolution.Initialize(_player, _evolutionsDatabase.BasicChance);
            evolution.OnLevelUp += OnEvolutionLevelUp;
        }
    }
    
    private void OnLevelUpdated(int level)
    {
        if (_evolutions.Count(evolution => evolution.State == EvolutionState.IsAble) <= 0) return;
        
        FillSlots();
        _movement.Disable();
    }

    public void ChooseEvolution(Evolution evolution)
    {
        evolution.Apply();
        _stats.AddSource(evolution);
        _abilities.Add(evolution.Config.Abilities);

        UnlockEvolutions();
        BlockEvolutions(evolution);
        UpdateChances(evolution);
        
        OnEvolutionApplied?.Invoke(evolution);
        
        _movement.Enable();
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

    private void UpdateChances(Evolution evolution)
    {
        foreach (var evo in _evolutions.Where(evo => evo.Config.CreatureType == evolution.Config.CreatureType))
        {
            evo.IncreaseChance(_evolutionsDatabase.ChanceScaler);
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
            var totalWeight = availableEvolutions.Sum(evolution => evolution.Chance);

            var randomValue = Random.Range(0, totalWeight);

            var currentWeight = 0;
            for (var j = 0; j < availableEvolutions.Count; j++)
            {
                currentWeight += availableEvolutions[j].Chance;

                if (randomValue >= currentWeight) continue;
                
                evolutions.Add(availableEvolutions[j]);
                
                availableEvolutions.RemoveAt(j); 
                break;
            }
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