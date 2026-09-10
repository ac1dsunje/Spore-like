using System;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class ParticlesModule: IStartable, IDisposable
{
    private readonly ParticlesSpawner _particles;
    private readonly HealthModule _health;
    private readonly AnimationSettings _config;
    private readonly Transform _transform;

    public ParticlesModule(ParticlesSpawner particlesSpawner, HealthModule health, AnimationSettings config,
        Transform transform)
    {
        _particles = particlesSpawner;
        _health = health;
        _config = config;
        _transform = transform;
    }
    
    public void Start()
    {
        _health.OnDamageTaken += SpawnParticles;
    }
    
    private void SpawnParticles(float damage)
    {
        if (damage <= 0f) return;
        _particles.Spawn(
            _config.OnHitParticles, 
            _transform.position, 
            _config.Color
        );
    }

    public void Dispose()
    {
        _health.OnDamageTaken -= SpawnParticles;
    }
}
}