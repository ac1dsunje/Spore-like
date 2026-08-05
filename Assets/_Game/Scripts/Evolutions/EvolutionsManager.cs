using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Evolutions.UI.Choosing;
using _Game.Scripts.Player;
using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class EvolutionsManager: MonoBehaviour
{
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions = 3;
    private PlayerModel _player;
    private EvolutionChooseScreen _screen;
    
    private readonly List<Evolution> _evolutions = new();
    
    public void Construct(PlayerModel player, EvolutionChooseScreen screen)
    {
        _player = player;
        _player.Experience.OnLevelChanged += OnLevelUpdated;
        
        _screen = screen;
        _screen.OnEvolutionChosen += OnEvolutionChosen;

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
        _screen.Show();
        _player.Movement.Disable();
    }

    private void OnEvolutionChosen(Evolution evolution)
    {
        evolution.Apply();
        _player.Stats.AddEvolution(evolution);
        _player.Abilities.Add(evolution.Config.Abilities);

        UnlockEvolutions();
        BlockEvolutions(evolution);
        UpdateChances(evolution);
        
        _screen.Hide();
        _player.Movement.Enable();
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
        
        _screen.SetSlots(evolutions);
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

    private void OnDestroy()
    {
        _player.Experience.OnLevelChanged -= OnLevelUpdated;
        _screen.OnEvolutionChosen -= OnEvolutionChosen;

        foreach (var evolution in _evolutions)
        {
            evolution.OnLevelUp -= OnEvolutionLevelUp;
            evolution.Dispose();
        }
    }
}
}