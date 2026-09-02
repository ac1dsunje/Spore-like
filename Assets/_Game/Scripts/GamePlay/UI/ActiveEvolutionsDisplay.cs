using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveEvolutionsDisplay: MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _container;
    
    private EvolutionsModule _player;

    [Inject] private EvolutionFormatter _formatter;

    public event Action<Sprite, string, string> OnEvolutionHovered;
    public event Action OnEvolutionUnhovered;

    private readonly List<ActiveEvolutionSlotUI> _slots = new();

    public void Construct(EvolutionsModule player)
    {
        _player = player;
        _player.OnEvolutionApplied += AddEvolution;
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_slotPrefab, _container).GetComponent<ActiveEvolutionSlotUI>();

        slot.Construct(evolution, _formatter);

        slot.OnEvolutionHovered += OnEvolutionHovered;
        slot.OnEvolutionUnhovered += OnEvolutionUnhovered;

        _slots.Add(slot);
    }

    private void OnDestroy()
    {
        if (_player != null)
            _player.OnEvolutionApplied -= AddEvolution;

        foreach (var slot in _slots)
        {
            slot.OnEvolutionHovered -= OnEvolutionHovered;
            slot.OnEvolutionUnhovered -= OnEvolutionUnhovered;
        }
    }
}
}