using System.Collections.Generic;
using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.UI;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions.UI.Choosing
{
public class EvolutionChooseUIScreen : UIScreen
{
    [SerializeField] private GameObject _slotPrefab;
    private EvolutionsManager _evolutionsManager;
    
    private readonly List<EvolutionSlotUI> _slots = new();

    protected override void Awake()
    {
        base.Awake();
        HideScreen();
    }

    public void Construct(EvolutionsManager evolutionsManager)
    {
        _evolutionsManager = evolutionsManager;
        _evolutionsManager.OnSlotsFilled += SetSlots;
    }

    public void Show() => ShowScreen();

    public void Hide() => HideScreen();

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
        var slot = Instantiate(_slotPrefab, transform).GetComponent<EvolutionSlotUI>();
        slot.SetEvolution(evolution);
        _slots.Add(slot);
        slot.OnSlotClicked += EvolutionChosen;
    }

    private void EvolutionChosen(Evolution evolution)
    {
        _evolutionsManager.ChooseEvolution(evolution);
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
        _evolutionsManager.OnSlotsFilled -= SetSlots;
    }
}
}