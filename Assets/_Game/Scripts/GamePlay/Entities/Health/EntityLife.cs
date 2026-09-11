using System;
using _Game.Scripts.GamePlay.Drops;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules.Health;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public class EntityLife : IStartable, IDisposable
{
    private readonly EntitiesRegistry _entitiesRegistry;
    private readonly ExperienceModule _experience;
    private readonly HealthModule _health;
    private readonly LifeModule _life;
    private readonly DropSpawner _dropSpawner;
    private readonly DropsConfig _dropConfigs;
    private readonly Transform _transform;
    
    private IDisposable _healthSubscription;
    
    public EntityLife(EntitiesRegistry entitiesRegistry, ExperienceModule experience, HealthModule health, 
        LifeModule life, DropSpawner dropSpawner, DropsConfig dropConfig, Transform transform)
    {
        _entitiesRegistry = entitiesRegistry;
        _experience = experience;
        _health = health;
        _life = life;
        _dropSpawner = dropSpawner;
        _dropConfigs = dropConfig;
        _transform = transform;
    }

    public void Start()
    {
        _healthSubscription = _health.Current
            .Where(value => value <= 0)
            .Subscribe(_ => _life.TryConsumeLife());
        
        _life.OnRevived += OnRevived;
        _life.OnDeath += OnDeath;
    }

    private void OnRevived(LifeModule life)
    {
        _health.Reset();
    }

    private void OnDeath(LifeModule life)
    {
        Die(_health);
    }

    private void Die(HealthModule health)
    {
        _dropSpawner.Spawn(_experience.Level, _transform.position, _dropConfigs);
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _healthSubscription?.Dispose();
        _life.OnRevived -= OnRevived;
        _life.OnDeath -= OnDeath;
    }
}
}