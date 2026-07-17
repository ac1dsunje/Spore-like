using System.Collections.Generic;
using _Game.Scripts.Player;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Evolutions.UI
{
public class EvolutionChooseScreen : ScreenManager
{
    [SerializeField] private EvolutionSlotUI[] _slots;
    [SerializeField] private EvolutionConfig[] _evolutions;
    [SerializeField] private EvolutionRarityConfig[] _rarities;
    private PlayerController _player;
    
    public void Construct(PlayerController player)
    {
        _player = player;
        _player.OnLevelChanged += OnLevelUpdated;
    }

    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked += EvolutionChosen;
        }
    }

    private void EvolutionChosen(Evolution evolution)
    {
        Debug.Log($"Evolution {evolution.Name} clicked");
        
        // TODO: apply
        
        HideScreen();
        Time.timeScale = 1;
    }

    private void Start()
    {
        HideScreen();
    }
    
    private void OnLevelUpdated(int level)
    {
        ShowScreen();

        GenerateEvolutions();
        
        Time.timeScale = 0;
    }

    private void GenerateEvolutions()
    {
        var availableEvolutions = new List<EvolutionConfig>(_evolutions);
        
        var slotsToFill = Mathf.Min(_slots.Length, availableEvolutions.Count);

        for (var i = 0; i < slotsToFill; i++)
        {
            var randomEvolutionIndex = Random.Range(0, availableEvolutions.Count);
            var randomRarityIndex = Random.Range(0, _rarities.Length);
            var chosen = new Evolution(availableEvolutions[randomEvolutionIndex], _rarities[randomRarityIndex]);
            
            _slots[i].SetBuff(chosen); 
            
            availableEvolutions.RemoveAt(randomEvolutionIndex);
        }
    }

    private void OnDestroy()
    {
        _player.OnLevelChanged -= OnLevelUpdated;
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= EvolutionChosen;
        }
    }
}
}