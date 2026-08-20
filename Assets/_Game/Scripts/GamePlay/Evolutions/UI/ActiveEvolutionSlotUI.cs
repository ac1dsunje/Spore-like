using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
[RequireComponent(typeof(Button))]
public class ActiveEvolutionSlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    public event Action<string, string> OnEvolutionClicked;
    
    private Button _button;
    
    private Evolution _evolution;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    public void Construct(Evolution evolution)
    {
        _evolution = evolution;
        _evolution.OnRarityChanged += UpdateFrame;
        
        UpdateSprite();
        UpdateFrame();
        
        _button.onClick.AddListener(OnMouseClick);
    }

    private void OnMouseClick()
    {
        var description = $"some description here";
        
        OnEvolutionClicked?.Invoke(_evolution.Name, description);
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
        _evolution.OnRarityChanged -= UpdateFrame;
        _button.onClick.RemoveAllListeners();
    }
}
}