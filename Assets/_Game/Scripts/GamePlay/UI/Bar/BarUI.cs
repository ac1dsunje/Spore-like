using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public class BarUI: MonoBehaviour
{
    [SerializeField] protected Image Bar;
    [SerializeField] protected Image Icon;
    private BarConfig _config;
    private IResource _module;
    
    public void Construct(IResource module, BarConfig config)
    {
        _config = config;
        _module = module;
        UpdateBar(_config.MaxValue? 1: 0, 1);
        _module.OnValueChanged += UpdateBar;
        Bar.color = _config.Color;
        Icon.sprite = _config.Sprite;
    }

    private void UpdateBar(float min, float max) => Bar.fillAmount = min/max;

    private void OnDestroy() => _module.OnValueChanged -= UpdateBar;
}
}