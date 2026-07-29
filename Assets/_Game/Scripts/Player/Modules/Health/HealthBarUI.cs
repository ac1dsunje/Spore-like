using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Player.Modules.Health
{
public class HealthBarUI: MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private HealthModule _module;
    
    public void Construct(HealthModule module)
    {
        _module = module;
        _module.OnHealthChanged += UpdateBar;
    }

    private void UpdateBar(float amount, float maxHealth)
    {
        _healthBar.fillAmount = amount/maxHealth;
    }

    private void OnDestroy()
    {
        _module.OnHealthChanged -= UpdateBar;
    }
}
}