using System;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Modules.Health;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class ParticlesModule : IStartable, IDisposable
{
    private readonly ParticlesSpawner _particles;
    private readonly HealthModule _health;
    private readonly AnimationSettings _config;
    private readonly Transform _transform;
    private IDisposable _subscription;

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
        _subscription = _health.Current
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Previous - pair.Current;
                if (delta > 0)
                {
                    SpawnParticles();
                }
            });
    }
    
    private void SpawnParticles()
    {
        _particles.Spawn(
            _config.OnHitParticles, 
            _transform.position, 
            _config.Color
        );
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
}