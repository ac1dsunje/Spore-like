using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class OverlayUIScreen: UIScreen
{
    [SerializeField] private HealthBarUI _healthBarUI;
    [SerializeField] private ExperienceBarUI _experienceBarUI;
    [SerializeField] private EnduranceBarUI _enduranceBarUI;

    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);
    }
}
}