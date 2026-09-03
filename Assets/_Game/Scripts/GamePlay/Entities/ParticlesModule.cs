using System;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class ParticlesModule: IStartable, IDisposable
{
    [Inject] private ParticlesSpawner _particles;
    [Inject] private HealthModule _health;
    [Inject] private AnimationSettings _config;
    [Inject] private MovementModule _movement;
    
    public void Start()
    {
        _health.OnDamageTaken += SpawnParticles;
    }
    
    private void SpawnParticles(float damage)
    {
        if (damage <= 0f) return;
        _particles.Spawn(
            _config.OnHitParticles, 
            _movement.Transform.position, 
            _config.Color
        );
    }

    public void Dispose()
    {
        _health.OnDamageTaken -= SpawnParticles;
    }
}
}