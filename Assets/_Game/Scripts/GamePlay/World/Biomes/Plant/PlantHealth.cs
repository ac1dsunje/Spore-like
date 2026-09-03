using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Biomes.Plant
{
public class PlantHealth: IHealthController
{
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        hit.Source?.SetDamageDealt(damage);
    }
}
}