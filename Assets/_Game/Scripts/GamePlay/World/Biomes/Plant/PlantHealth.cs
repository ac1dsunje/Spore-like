using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Biomes.Plant
{
public class PlantHealth: IStartable, IDisposable, IHealthController
{
    [Inject] private EntityConfig _config;
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private MovementModule _movement;
    [Inject] private ExperienceModule _experience;
    [Inject] private ParticlesSpawner _particles;
    [Inject] private EntitiesRegistry _entitiesRegistry;

    public void Start()
    {
        _health.OnDeath += Die;
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        hit.Source?.SetDamageDealt(damage);
        SpawnParticles();
    }

    private void SpawnParticles()
    {
        _particles.Spawn(
            _config.AnimationSettings.OnHitParticles, 
            _movement.Transform.position, 
            _config.AnimationSettings.Color
            );
    }

    private void Die(HealthModule health)
    {
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}