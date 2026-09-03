using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinHealth: IHealthController
{
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private IDamageSource _damageSource;

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }
}
}