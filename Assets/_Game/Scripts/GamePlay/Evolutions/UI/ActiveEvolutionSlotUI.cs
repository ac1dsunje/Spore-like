using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI
{
public class ActiveEvolutionSlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    private Evolution _evolution;
    
    public void Construct(Evolution evolution)
    {
        _evolution = evolution;
        _evolution.OnRarityChanged += UpdateFrame;
        UpdateSprite();
        UpdateFrame();
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
    }
}
}