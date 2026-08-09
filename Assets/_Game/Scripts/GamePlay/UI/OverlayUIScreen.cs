using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class OverlayUIScreen: UIScreen
{
    [SerializeField] private BarUI _healthBarUI;
    [SerializeField] private BarUI _experienceBarUI;
    [SerializeField] private BarUI _enduranceBarUI;

    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);
    }
}
}