using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodHealth: IStartable, IDisposable
{
    [Inject] private EntityConfig _config;
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private MovementModule _movement;
    [Inject] private BodyHitbox _hitBox;
    [Inject] private ParticlesSpawner _particles;
    [Inject] private EntitiesRegistry _entitiesRegistry;

    public void Start()
    {
        _health.OnDamageTaken += SpawnParticles;
        _health.OnDeath += Die;
        _hitBox.OnBite += TakeBite;
    }

    private void TakeBite(float damage, float penetration)
    {
        var appliedDamage = _defense.ApplyResistance(damage, penetration);
        _health.TakeDamage(appliedDamage);
    }

    private void SpawnParticles(float dmg)
    {
        _particles.Spawn(
            _config.AnimationSettings.OnHitParticles, 
            _movement.Transform.position, 
            _config.AnimationSettings.Color
            );
    }

    private void Die(HealthModule health)
    {
        _hitBox.SetEaten(_config.ExperienceAmount);
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _health.OnDamageTaken -= SpawnParticles;
        _health.OnDeath -= Die;
    }
}
}