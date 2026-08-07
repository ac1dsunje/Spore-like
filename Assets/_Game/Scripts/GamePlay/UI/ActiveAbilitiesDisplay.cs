using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveAbilitiesDisplay: MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private GameObject _abilitiesSlotPrefab;
    [SerializeField] private Transform  _abilitiesParent;
    
    private PlayerModel _player;

    public void Construct(PlayerModel player)
    {
        _player = player;
        _player.Abilities.OnAbilityAdded += AddAbility;
    }

    private void AddAbility(AbilityConfig ability)
    {
        var slot = Instantiate(_abilitiesSlotPrefab, _abilitiesParent).GetComponent<ActiveAbilitySlotUI>();
        slot.Construct(ability);
    }

    private void OnDestroy()
    {
        _player.Abilities.OnAbilityAdded -= AddAbility;
    }
}
}