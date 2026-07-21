using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Evolutions.UI
{
public class ActiveEvolutionSlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    private Evolution _evolution;
    
    public void Construct(Evolution evolution)
    {
        _evolution = evolution;
        _evolution.OnRarityChanged += UpdateSlot;
        UpdateSlot();
    }

    private void UpdateSlot()
    {
        _image.sprite = _evolution.Config.Sprite;
        _frame.sprite = _evolution.Frame;
    }

    private void OnDestroy()
    {
        _evolution.OnRarityChanged -= UpdateSlot;
    }
}
}