using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: MonoBehaviour, IDamageAble
{
    private HealthModule _healthModule;
    private DefenseModule _defenseModule;
    private AttackModule _attackModule;
    private EntityStats _entityStats;

    [Inject]
    private void Construct(EntityStats entityStats, EntityStatsConfig entityStatsConfig, DefenseModule defenseModule,
        HealthModule healthModule, AttackModule attackModule, SeaUrchinAttackBehaviour attackBehaviour)
    {
        attackBehaviour.SetOwner(this);
        _entityStats = entityStats;
        _defenseModule = defenseModule;
        _healthModule = healthModule;
        _attackModule = attackModule;

        _healthModule.OnDeath += Die;
        
        _entityStats.Initialize(entityStatsConfig.InitialConfigs);
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