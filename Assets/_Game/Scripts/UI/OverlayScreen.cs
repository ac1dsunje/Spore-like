using _Game.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayScreen: ScreenManager
{
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _levelText;
    
    private PlayerStats _player;

    public void Construct(PlayerStats player)
    {
        _player = player;
        _player.OnExperienceChanged += UpdateExperience;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void UpdateExperience(int amount)
    {
        _experienceText.text = $"Experience: {amount}";
    }

    private void UpdateLevel(int amount)
    {
        _levelText.text = $"Level: {amount}";
    }

    private void OnDestroy()
    {
        _player.OnExperienceChanged -= UpdateExperience;
        _player.OnLevelChanged -= UpdateLevel;
    }
}
}