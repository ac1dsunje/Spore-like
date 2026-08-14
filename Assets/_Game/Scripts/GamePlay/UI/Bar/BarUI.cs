using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public class BarUI: MonoBehaviour
{
    [SerializeField] protected Image Bar;
    [SerializeField] private bool _maxValue;
    
    private IResource _module;
    
    public void Construct(IResource module)
    {
        _module = module;
        UpdateBar(_maxValue? 1: 0, 1);
        _module.OnValueChanged += UpdateBar;
    }

    private void UpdateBar(float min, float max) => Bar.fillAmount = min/max;

    private void OnDestroy() => _module.OnValueChanged -= UpdateBar;
}
}