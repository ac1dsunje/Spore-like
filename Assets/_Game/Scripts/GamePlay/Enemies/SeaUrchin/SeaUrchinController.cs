using _Game.Scripts.GamePlay.Animation;
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
        HealthModule healthModule, AttackModule attackModule, SeaUrchinAttackBehaviour attackBehaviour,
        AnimationSettings animationSettings, ItemAnimation itemAnimation)
    {
        attackBehaviour.SetOwner(this);
        _entityStats = entityStats;
        _defenseModule = defenseModule;
        _healthModule = healthModule;
        _attackModule = attackModule;
        
        itemAnimation.SetConfig(animationSettings);

        _healthModule.OnDeath += Die;
        
        _entityStats.Initialize(entityStatsConfig.InitialConfigs);
    }

    public float TakeDamage(HitInfo hit)
    {
        var damage = _defenseModule.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _healthModule.TakeDamage(damage);
        var returnedDamage = _defenseModule.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, _attackModule.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
        return damage;
    }

    public void SetDamageDealt(float damage) {}

    private void Die()
    {
        _healthModule.OnDeath -= Die;
        Destroy(gameObject);
    }
}
}