using _Game.Scripts.Abilities;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Endurance;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class OverlayScreen: ScreenManager
{
    [SerializeField] private HealthBarUI _healthBarUI;
    [SerializeField] private ExperienceBarUI _experienceBarUI;
    [SerializeField] private EnduranceBarUI _enduranceBarUI;
    
    [Header("Evolutions")]
    [SerializeField] private GameObject _evolutionSlotPrefab;
    [SerializeField] private Transform  _evolutionsParent;
    
    [Header("Evolutions")]
    [SerializeField] private GameObject _abilitiesSlotPrefab;
    [SerializeField] private Transform  _abilitiesParent;
    
    private PlayerModel _player;

    public void Construct(PlayerModel player)
    {
        _player = player;
        _player.Stats.OnEvolutionAdded += AddEvolution;
        _player.Abilities.OnAbilityAdded += AddAbility;
        
        _healthBarUI.Construct(_player.Health);
        _experienceBarUI.Construct(_player.Experience);
        _enduranceBarUI.Construct(_player.Endurance);
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_evolutionSlotPrefab, _evolutionsParent).GetComponent<ActiveEvolutionSlotUI>();
        slot.Construct(evolution);
    }

    private void AddAbility(AbilityConfig ability)
    {
        var slot = Instantiate(_abilitiesSlotPrefab, _abilitiesParent).GetComponent<ActiveAbilitySlotUI>();
        slot.Construct(ability);
    }

    private void OnDestroy()
    {
        _player.Stats.OnEvolutionAdded -= AddEvolution;
        _player.Abilities.OnAbilityAdded -= AddAbility;
    }
}
}