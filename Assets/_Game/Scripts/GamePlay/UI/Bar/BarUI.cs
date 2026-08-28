using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public class BarUI: MonoBehaviour
{
    [SerializeField] protected Image Bar;
    [SerializeField] protected Image Icon;
    [SerializeField] protected BarConfig Config;
    private IResource _module;
    
    public void Construct(IResource module)
    {
        _module = module;
        UpdateBar(Config.MaxValue? 1: 0, 1);
        _module.OnValueChanged += UpdateBar;
        Bar.color = Config.Color;
        Icon.sprite = Config.Sprite;
    }

    private void UpdateBar(float min, float max) => Bar.fillAmount = min/max;

    private void OnDestroy() => _module.OnValueChanged -= UpdateBar;
}
}