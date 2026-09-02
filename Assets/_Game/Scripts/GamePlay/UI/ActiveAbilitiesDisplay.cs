using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Entities;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveAbilitiesDisplay: MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform  _container;
    
    private AbilitiesModule _player;

    public void Construct(AbilitiesModule player)
    {
        _player = player;
        _player.OnAbilityAdded += AddAbility;
    }

    private void AddAbility(AbilityConfig ability)
    {
        var slot = Instantiate(_slotPrefab, _container).GetComponent<ActiveAbilitySlotUI>();
        slot.Construct(ability);
    }

    private void OnDestroy()
    {
        _player.OnAbilityAdded -= AddAbility;
    }
}
}