using System.Collections.Generic;
using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Entities;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Evolutions.UI.Choosing
{
public class EvolutionChooseUIScreen : UIScreen
{
    [SerializeField] private GameObject _slotPrefab;
    private EvolutionsModule _evolutionsModule;
    
    [Inject] private EvolutionFormatter _formatter;
    
    private readonly List<ChoosingEvolutionSlotUI> _slots = new();

    protected override void Awake()
    {
        base.Awake();
        HideScreen();
    }

    public void Construct(EvolutionsModule evolutionsModule)
    {
        _evolutionsModule = evolutionsModule;
        _evolutionsModule.OnSlotsFilled += SetSlots;
    }

    private void SetSlots(List<Evolution> evolutions)
    {
        ClearSlots();
        foreach (var evolution in evolutions)
        {
            CreateSlot(evolution);
        }
    }

    private void CreateSlot(Evolution evolution)
    {
        var slot = Instantiate(_slotPrefab, transform).GetComponent<ChoosingEvolutionSlotUI>();
        slot.SetEvolution(evolution, _formatter);
        _slots.Add(slot);
        slot.OnSlotClicked += EvolutionChosen;
    }

    private void EvolutionChosen(Evolution evolution)
    {
        _evolutionsModule.ChooseEvolution(evolution);
        ClearSlots();
        HideScreen();
    }

    private void ClearSlots()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= EvolutionChosen;
            Destroy(slot.gameObject);
        }
        _slots.Clear();
    }
    
    private void OnDestroy()
    {
        ClearSlots();
        _evolutionsModule.OnSlotsFilled -= SetSlots;
    }
}
}