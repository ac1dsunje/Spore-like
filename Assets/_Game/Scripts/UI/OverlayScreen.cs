using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI;
using _Game.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayScreen: ScreenManager
{
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _levelText;
    
    [SerializeField] private GameObject _evolutionSlotPrefab;
    [SerializeField] private Transform  _evolutionsParent;
    
    private PlayerStats _player;

    public void Construct(PlayerStats player)
    {
        _player = player;
        _player.Experience.OnExperienceChanged += UpdateExperience;
        _player.Experience.OnLevelChanged += UpdateLevel;
        _player.OnEvolutionAdded += AddEvolution;
    }

    private void UpdateExperience(int amount)
    {
        _experienceText.text = $"Experience: {amount}";
    }

    private void UpdateLevel(int amount)
    {
        _levelText.text = $"Level: {amount}";
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_evolutionSlotPrefab, _evolutionsParent).GetComponent<ActiveEvolutionSlotUI>();
        slot.Construct(evolution);
    }

    private void OnDestroy()
    {
        _player.Experience.OnExperienceChanged -= UpdateExperience;
        _player.Experience.OnLevelChanged -= UpdateLevel;
        _player.OnEvolutionAdded -= AddEvolution;
    }
}
}