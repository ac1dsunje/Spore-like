using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI;
using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveEvolutionsDisplay: MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform  _container;
    
    private EvolutionsModule _player;

    public event Action<Sprite, string, string> OnEvolutionHovered;
    private readonly List<ActiveEvolutionSlotUI> _slots = new();

    public void Construct(EvolutionsModule player)
    {
        _player = player;
        _player.OnEvolutionApplied += AddEvolution;
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_slotPrefab, _container).GetComponent<ActiveEvolutionSlotUI>();
        slot.Construct(evolution);
        slot.OnEvolutionHovered += OnEvolutionHovered;
        _slots.Add(slot);
    }

    private void OnDestroy()
    {
        _player.OnEvolutionApplied -= AddEvolution;
        foreach (var slot in _slots)
        {
            slot.OnEvolutionHovered -= OnEvolutionHovered;
        }
    }
}
}