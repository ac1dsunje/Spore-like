using System.Collections.Generic;
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

    public void Construct(PlayerController player, EvolutionChooseScreen screen)
    {
        _player = player;
        _player.OnLevelChanged += OnLevelUpdated;
        
        _screen = screen;
        _screen.OnEvolutionChosen += OnEvolutionChosen;
    }
    
    private void OnLevelUpdated(int level)
    {
        _screen.Show();

        GenerateEvolutions();
        
        Time.timeScale = 0;
    }

    private void OnEvolutionChosen(Evolution evolution)
    {
        // TODO: apply
        
        _screen.Hide();
        Time.timeScale = 1;
    }

    private void GenerateEvolutions()
    {
        var availableEvolutions = new List<EvolutionConfig>(_evolutionsDatabase.Evolutions);
        
        var slotsToFill = Mathf.Min(_minEvolutions, availableEvolutions.Count);
        
        var evolutions = new List<Evolution>(slotsToFill);

        for (var i = 0; i < slotsToFill; i++)
        {
            var randomEvolutionIndex = Random.Range(0, availableEvolutions.Count);
            var chosen = new Evolution(availableEvolutions[randomEvolutionIndex]);
            chosen.SetRarity(GetRandomRarity());
            
            evolutions.Add(chosen);
            
            availableEvolutions.RemoveAt(randomEvolutionIndex);
        }
        
        _screen.SetSlots(evolutions);
    }
    
    private EvolutionRarityConfig GetRandomRarity()
    {
        var rarities = _evolutionsDatabase.Rarities;

        var totalWeight = 0f;
        
        foreach (var rarity in rarities)
        {
            totalWeight += rarity.Chance;
        }

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