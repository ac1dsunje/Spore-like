using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    
    private AttackModule _module;
    private PlayerInputService _inputService;

    private IDamageAble _owner;

    public void Construct(AttackModule module, PlayerInputService inputService, IDamageAble owner)
    {
        _module = module;
        _inputService = inputService;
        _owner = owner;
    }

    private void Update()
    {
        if (_inputService.AttackPressed)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _meleeWeaponObject.gameObject.SetActive(true);
        UpdateAttackPosition();
        var hit = new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, _owner);
        _meleeWeaponObject.SetHit(hit);
    }

    private void UpdateAttackPosition()
    {
        var mousePosition = _inputService.MousePosition;
        Vector2 playerPosition = transform.position;
        var direction = (mousePosition - playerPosition).normalized;

        _meleeWeaponObject.transform.localPosition = direction * _module.AttackRange;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _meleeWeaponObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}