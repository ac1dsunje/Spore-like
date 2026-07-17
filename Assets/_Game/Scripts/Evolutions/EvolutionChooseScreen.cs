using System.Collections.Generic;
using _Game.Scripts.Player;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class EvolutionChooseScreen : ScreenManager
{
    [SerializeField] private EvolutionSlotUI[] _slots;
    [SerializeField] private EvolutionConfig[] _evolutions;
    private PlayerController _player;
    
    public void Construct(PlayerController player)
    {
        _player = player;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked += EvolutionChosen;
        }
    }

    private void EvolutionChosen(EvolutionConfig evolutionConfig)
    {
        Debug.Log($"Evolution {evolutionConfig.Name} clicked");
        
        // TODO: apply
        
        HideScreen();
        Time.timeScale = 1;
    }

    private void Start()
    {
        HideScreen();
    }
    
    private void UpdateLevel(int level)
    {
        ShowScreen();
        
        var availableEvolutions = new List<EvolutionConfig>(_evolutions);
        
        var slotsToFill = Mathf.Min(_slots.Length, availableEvolutions.Count);

        for (var i = 0; i < slotsToFill; i++)
        {
            var randomIndex = Random.Range(0, availableEvolutions.Count);
            var chosen = availableEvolutions[randomIndex];
            
            _slots[i].SetBuff(chosen); 
            
            availableEvolutions.RemoveAt(randomIndex);
        }
        
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        _player.OnLevelChanged -= UpdateLevel;
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= EvolutionChosen;
        }
    }
}
}