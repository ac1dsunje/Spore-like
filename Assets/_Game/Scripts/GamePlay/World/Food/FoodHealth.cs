using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodHealth: MonoBehaviour
{
    private EntityConfig _config;
    
    private HealthModule _health;
    private DefenseModule _defense;
    private MovementModule _movement;
    private BodyHitbox _hitBox;
    private ParticlesSpawner _particles;

    [Inject]
    private void Construct(HealthModule health, DefenseModule defense, MovementModule movement,
        EntityConfig config, BodyHitbox hitbox, ParticlesSpawner particlesSpawner)
    {
        _health = health;
        _defense = defense;
        _movement = movement;
        _config = config;
        _hitBox = hitbox;
        _particles = particlesSpawner;
        
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
        _particles.Spawn(_config.AnimationSettings.OnHitParticles, _movement.Transform.position, _config.AnimationSettings.Color);
    }

    private void Die()
    {
        _hitBox.SetEaten(_config.ExperienceAmount);
        
        Destroy(gameObject, 0.5f);
        
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        _health.OnDamageTaken -= SpawnParticles;
        _health.OnDeath -= Die;
    }
}
}