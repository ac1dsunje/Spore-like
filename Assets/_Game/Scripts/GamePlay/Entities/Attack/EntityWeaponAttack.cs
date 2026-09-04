using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponAttack : ITickable, IDamageSource, IAttackController
{
    [Inject] private AttackModule _attack;
    [Inject] private MovementModule _movement;
    [Inject] private IDamageReceiver _receiver;
    [Inject] private EntityWeaponHolder _weapon;
    [Inject] private ProjectileConfig _projectileConfig;

    private float _attackCooldownTimer;
    private bool CanAttack => _attackCooldownTimer <= 0f;

    public void Tick()
    {
        if (_attackCooldownTimer > 0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 mousePosition)
    {
        if (!CanAttack) return;
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _weapon.SetAttack(mousePosition, _movement.Transform.position, hit, _attack.AttackRange, _projectileConfig);
        _attackCooldownTimer = _attack.AttackSpeed;
    }
    
    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);
}
}