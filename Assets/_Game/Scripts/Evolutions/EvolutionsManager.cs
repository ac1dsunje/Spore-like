using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Evolutions.UI;
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
    private PlayerController _player;
    private EvolutionChooseScreen _screen;
    
    private List<Evolution> _evolutions = new();
    
    public void Construct(PlayerController player, EvolutionChooseScreen screen)
    {
        _player = player;
        _player.Stats.OnLevelChanged += OnLevelUpdated;
        
        _screen = screen;
        _screen.OnEvolutionChosen += OnEvolutionChosen;

        _evolutions = _evolutionsDatabase.GenerateEvolutions();
    }
    
    private void OnLevelUpdated(int level)
    {
        if (_evolutions.Count(evolution => evolution.State == EvolutionState.IsAble) <= 0) return;
        
        FillSlots();
        _screen.Show();
        _player.Disable();
    }

    private void OnEvolutionChosen(Evolution evolution)
    {
        evolution.Apply();
        _player.Stats.AddEvolution(evolution);

        UnlockEvolutions(evolution);
        BlockEvolutions(evolution);
        
        _screen.Hide();
        _player.Enable();
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
            var randomEvolutionIndex = Random.Range(0, availableEvolutions.Count);
            var chosen = availableEvolutions[randomEvolutionIndex];
            
            evolutions.Add(chosen);
            
            availableEvolutions.RemoveAt(randomEvolutionIndex);
        }

        return evolutions;
    }

    private void OnDestroy()
    {
        _player.Stats.OnLevelChanged -= OnLevelUpdated;
        _screen.OnEvolutionChosen -= OnEvolutionChosen;
    }
}
}