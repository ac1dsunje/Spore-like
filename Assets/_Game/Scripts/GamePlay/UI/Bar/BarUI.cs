using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public class BarUI : MonoBehaviour
{
    [SerializeField] protected Image Bar;
    [SerializeField] protected Image Icon;
    
    private float _currentValue;
    private float _maxValue;
    
    public void Construct(ReadOnlyReactiveProperty<float> current, ReadOnlyReactiveProperty<float> max, BarConfig config)
    {
        Bar.color = config.Color;
        Icon.sprite = config.Sprite;
            
        current.Subscribe(value =>
        {
            _currentValue = value;
            UpdateBar();
        }).AddTo(this);
        
        max.Subscribe(value =>
        {
            _maxValue = value;
            UpdateBar();
        }).AddTo(this);
    }
        
    private void UpdateBar()
    {
        Bar.fillAmount = _maxValue > 0 ? _currentValue / _maxValue : 0f;
    }
}
}