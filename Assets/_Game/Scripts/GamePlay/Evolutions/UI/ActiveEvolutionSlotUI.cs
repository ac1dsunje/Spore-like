using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public class ActiveEvolutionSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    private EvolutionFormatter _formatter;
    
    public event Action<Sprite, string, string> OnEvolutionHovered;
    public event Action OnEvolutionUnhovered;
    
    private Evolution _evolution;
    
    public void Construct(Evolution evolution, EvolutionFormatter formatter)
    {
        _evolution = evolution;
        _evolution.OnRarityChanged += UpdateFrame;
        _formatter = formatter;
        
        UpdateSprite();
        UpdateFrame();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEvolutionHovered?.Invoke(
            _evolution.Config.Sprite,
            _evolution.Name,
            _formatter.FormatDescription(_evolution)
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnEvolutionUnhovered?.Invoke();
    }

    private void UpdateSprite() => _image.sprite = _evolution.Config.Sprite;

    private void UpdateFrame() => _frame.sprite = _evolution.Frame;

    private void OnDestroy()
    {
        if (_evolution != null)
            _evolution.OnRarityChanged -= UpdateFrame;
    }
}
}