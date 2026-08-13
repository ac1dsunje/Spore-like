using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    
    private AttackModule _module;
    private PlayerInputService _inputService;
    private PlayerAuthority _authority;

    [Inject]
    private void Construct(AttackModule module, PlayerInputService inputService, PlayerAuthority authority)
    {
        _module = module;
        _inputService = inputService;
        _meleeWeaponObject.transform.SetParent(null);
        _authority = authority;
    }

    private void Update()
    {
        if (_inputService.AttackPressed && _authority.IsLocal)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _meleeWeaponObject.gameObject.SetActive(true);
        UpdateAttackPosition();
        var hit = new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, _module.Owner);
        _meleeWeaponObject.SetHit(hit);
    }

    private void UpdateAttackPosition()
    {
        var mouseWorld = _inputService.MouseWorldPosition;
        var playerPosition = (Vector2)transform.position;

        var offset = mouseWorld - playerPosition;
        var rawDistance = offset.magnitude;

        var distance = Mathf.Clamp(rawDistance, 0.5f, _module.AttackRange);

        var direction = rawDistance > Mathf.Epsilon ? offset.normalized : Vector2.right;

        _meleeWeaponObject.transform.position = playerPosition + direction * distance;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _meleeWeaponObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}