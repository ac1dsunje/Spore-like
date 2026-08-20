using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public class ActiveEvolutionSlotUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    public event Action<Sprite, string, string> OnEvolutionHovered;
    
    private Evolution _evolution;
    
    public void Construct(Evolution evolution)
    {
        _evolution = evolution;
        _evolution.OnRarityChanged += UpdateFrame;
        
        UpdateSprite();
        UpdateFrame();
    }
        
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEvolutionHovered?.Invoke(
            _evolution.Config.Sprite,
            _evolution.Name,
            EvolutionFormatter.FormatDescription(_evolution)
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    private void UpdateSprite()
    {
        _image.sprite = _evolution.Config.Sprite;
    }
    
    private void UpdateFrame()
    {
        _frame.sprite = _evolution.Frame;
    }

    private void OnDestroy()
    {
        if (_evolution != null)
            _evolution.OnRarityChanged -= UpdateFrame;
    }
}
}