using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Evolutions.UI;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class EvolutionsManager: MonoBehaviour
{
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private int _minEvolutions = 3;
    private PlayerController _player;
    private EvolutionChooseScreen _screen;
    
    private readonly List<Evolution> _evolutions = new();
    private readonly List<Evolution> _activeEvolutions = new();
    
    public void Construct(PlayerController player, EvolutionChooseScreen screen)
    {
        _player = player;
        _player.OnLevelChanged += OnLevelUpdated;
        
        _screen = screen;
        _screen.OnEvolutionChosen += OnEvolutionChosen;

        GenerateEvolutionsFromConfigs();
    }

    private void GenerateEvolutionsFromConfigs()
    {
        foreach(var evolution in _evolutionsDatabase.Evolutions)
        {
            var evo = evolution.CreateEvolution();
            evo.SetPlayer(_player);
            _evolutions.Add(evo);
        }
    }
    
    private void OnLevelUpdated(int level)
    {
        if (_evolutions.Count(evolution => evolution.State == EvolutionState.IsAble) <= 0) return;
        
        FillSlots();
        _screen.Show();
        _player.Stop();
    }

    private void OnEvolutionChosen(Evolution evolution)
    {
        evolution.Apply();
        
        _activeEvolutions.Add(evolution);

        UnlockEvolutions(evolution);
        BlockEvolutions(evolution);
        
        _screen.Hide();
        _player.Resume();
    }

    private void UnlockEvolutions(Evolution evolution)
    {
        foreach (var evo in _evolutions.Where(evo => evolution.Unlocks.Contains(evo.Config)))
        {
            evo.Unlock();
        }
    }
    
    private void BlockEvolutions(Evolution evolution)
    {
        foreach (var evo in _evolutions.Where(evo => evolution.Blocks.Contains(evo.Config)))
        {
            evo.Block();
        }
    }

    private void FillSlots()
    {
        var availableEvolutions = _evolutions.Where(evolution => evolution.State == EvolutionState.IsAble).ToList();

        var slotsToFill = Mathf.Min(_minEvolutions, availableEvolutions.Count);
        
        var evolutions = new List<Evolution>(slotsToFill);

        for (var i = 0; i < slotsToFill; i++)
        {
            var randomEvolutionIndex = Random.Range(0, availableEvolutions.Count);
            var chosen = availableEvolutions[randomEvolutionIndex];
            chosen.SetRarity(GetRandomRarity());
            
            evolutions.Add(chosen);
            
            availableEvolutions.RemoveAt(randomEvolutionIndex);
        }
        
        _screen.SetSlots(evolutions);
    }
    
    private EvolutionRarityConfig GetRandomRarity()
    {
        var rarities = _evolutionsDatabase.Rarities;

        var totalWeight = rarities.Sum(rarity => rarity.Chance);

        var randomValue = Random.Range(0f, totalWeight);

        var currentWeight = 0f;

        foreach (var rarity in rarities)
        {
            currentWeight += rarity.Chance;

            if (randomValue <= currentWeight)
            {
                return rarity;
            }
        }

        return rarities[rarities.Length - 1];
    }

    private void OnDestroy()
    {
        _player.OnLevelChanged -= OnLevelUpdated;
        _screen.OnEvolutionChosen -= OnEvolutionChosen;
    }
}
}