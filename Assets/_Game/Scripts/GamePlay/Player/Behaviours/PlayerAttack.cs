using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Entity.Interfaces;
using _Game.Scripts.GamePlay.Entity.Module;
using _Game.Scripts.GamePlay.Entity.Network;
using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerAttack : EntityNetworkBehaviour
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    
    private AttackModule _module;
    private PlayerInputService _inputService;
    private Ticker _ticker;

    private float _attackCooldownTimer;
    private bool _canAttack => _attackCooldownTimer <= 0f;

    [Inject]
    private void Construct(AttackModule module, PlayerInputService inputService, Ticker ticker)
    {
        _module = module;
        _inputService = inputService;
        _meleeWeaponObject.transform.SetParent(null);
        _ticker = ticker;
    }

    protected override void OnNetworkInitialized()
    {
        if (!IsLocal) return;
        _ticker.OnTick += CheckInput;
    }

    private void CheckInput(float timeDelta)
    {
        if (_attackCooldownTimer > 0f)
        {
            _attackCooldownTimer -= timeDelta;
        }

        if (!_canAttack) return;
        if (!_inputService.AttackPressed) return;

        Attack();
        _attackCooldownTimer = _module.AttackSpeed;
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