using System;
using System.Collections.Generic;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Evolutions.UI
{
public class EvolutionChooseScreen : ScreenManager
{
    [SerializeField] private EvolutionSlotUI[] _slots;
    
    public event Action<Evolution> OnEvolutionChosen;

    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked += EvolutionChosen;
        }

        Hide();
    }

    private void EvolutionChosen(Evolution evolution) => OnEvolutionChosen?.Invoke(evolution);

    public void Show() => ShowScreen();

    public void Hide() => HideScreen();

    public void SetSlots(List<Evolution> evolutions)
    {
        for(var i = 0; i < evolutions.Count; i++)
        {
            _slots[i].SetBuff(evolutions[i]);
        }
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