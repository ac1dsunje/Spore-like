using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Stats;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayScreen: ScreenManager
{
    [SerializeField] private HealthBarUI _healthBarUI;
    [SerializeField] private ExperienceBarUI _experienceBarUI;
    
    [SerializeField] private GameObject _evolutionSlotPrefab;
    [SerializeField] private Transform  _evolutionsParent;
    
    private PlayerModel _player;

    public void Construct(PlayerModel player)
    {
        _player = player;
        _player.Stats.OnEvolutionAdded += AddEvolution;
        
        _healthBarUI.Construct(_player.Health);
        _experienceBarUI.Construct(_player.Experience);
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_evolutionSlotPrefab, _evolutionsParent).GetComponent<ActiveEvolutionSlotUI>();
        slot.Construct(evolution);
    }

    private void OnDestroy()
    {
        _player.Stats.OnEvolutionAdded -= AddEvolution;
    }
}
}