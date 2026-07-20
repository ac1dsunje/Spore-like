using System;
using System.Collections.Generic;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Evolutions.UI
{
public class EvolutionChooseScreen : ScreenManager
{
    [SerializeField] private GameObject _slotPrefab;
    
    private readonly List<EvolutionSlotUI> _slots = new();
    
    public event Action<Evolution> OnEvolutionChosen;

    public void Show() => ShowScreen();

    public void Hide() => HideScreen();

    public void SetSlots(List<Evolution> evolutions)
    {
        foreach (var t in evolutions)
        {
            var slot = Instantiate(_slotPrefab, transform).GetComponent<EvolutionSlotUI>();
            slot.SetEvolution(t);
            _slots.Add(slot);
            slot.OnSlotClicked += EvolutionChosen;
        }
    }
    
    private void OnEnable() => Hide();

    private void EvolutionChosen(Evolution evolution)
    {
        OnEvolutionChosen?.Invoke(evolution);
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= EvolutionChosen;
            Destroy(slot.gameObject);
        }
        _slots.Clear();
    }

    private void OnDestroy()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= EvolutionChosen;
        }
    }
}
}