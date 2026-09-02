using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class BarsPanelUI : MonoBehaviour
{
    [SerializeField] private BarUI _healthBarUI;
    [SerializeField] private BarUI _experienceBarUI;
    [SerializeField] private BarUI _enduranceBarUI;
    [SerializeField] private BarUI _hungerBarUI;

    public void Construct(EntityController player)
    {
        _healthBarUI.Construct(player.Model.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Model.Endurance);
        _hungerBarUI.Construct(player.Model.Stomach);
    }
}
}