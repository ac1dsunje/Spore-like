using _Game.Scripts.GamePlay.Player;
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

    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);
        _hungerBarUI.Construct(player.Stomach);
    }
}
}