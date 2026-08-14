using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies
{
public class EnemyController: MonoBehaviour, IDamageAble
{
    private HealthModule _healthModule;
    private DefenseModule _defenseModule;
    private AttackModule _attackModule;
    private EntityStats _entityStats;

    [Inject]
    private void Construct(EntityStats entityStats, EntityStatsConfig entityStatsConfig, DefenseModule defenseModule,
        HealthModule healthModule, AttackModule attackModule)
    {
        _entityStats = entityStats;
        _defenseModule = defenseModule;
        _healthModule = healthModule;
        _attackModule = attackModule;

        _healthModule.OnDeath += Die;
        
        _entityStats.Initialize(entityStatsConfig.InitialConfigs);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryAttack(other);
    }

    private void TryAttack(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;
        damageAble.TakeDamage(new HitInfo(_attackModule.PhysicalDamage, _attackModule.IgnoreResistance, this));
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defenseModule.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _healthModule.TakeDamage(damage);
        var returnedDamage = _defenseModule.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, _attackModule.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
    }

    private void Die()
    {
        _healthModule.OnDeath -= Die;
        Destroy(gameObject);
    }
}
}