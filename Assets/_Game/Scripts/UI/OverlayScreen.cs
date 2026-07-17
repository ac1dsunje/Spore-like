using _Game.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayScreen: ScreenManager
{
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _levelText;
    
    private PlayerController _player;

    public void Construct(PlayerController player)
    {
        _player = player;
        _player.OnExperienceChanged += UpdateExperience;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void UpdateExperience(float amount)
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