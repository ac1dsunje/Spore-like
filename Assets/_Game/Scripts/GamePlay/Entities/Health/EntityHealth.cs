using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public class EntityHealth : IHealthController
{
    private readonly HealthModule _health;
    private readonly DefenseModule _defense;
    private readonly IDamageSource _damageSource;

    public EntityHealth(HealthModule health, DefenseModule defense, IDamageSource damageSource)
    {
        _health = health;
        _defense = defense;
        _damageSource = damageSource;
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.Reduce(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        var returnedHit = new HitInfo(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }
}
}