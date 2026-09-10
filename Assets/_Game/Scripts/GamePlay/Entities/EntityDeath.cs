using System;
using _Game.Scripts.GamePlay.Drops;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityDeath : IStartable, IDisposable
{
    private readonly EntitiesRegistry _entitiesRegistry;
    private readonly ExperienceModule _experience;
    private readonly HealthModule _health;
    private readonly DropSpawner _dropSpawner;
    private readonly DropsConfig _dropConfigs;
    private readonly Transform _transform;

    public EntityDeath(EntitiesRegistry entitiesRegistry, ExperienceModule experience, HealthModule health,
        DropSpawner dropSpawner, DropsConfig dropConfig, Transform transform)
    {
        _entitiesRegistry = entitiesRegistry;
        _experience = experience;
        _health = health;
        _dropSpawner = dropSpawner;
        _dropConfigs = dropConfig;
        _transform = transform;
    }

    public void Start()
    {
        _health.OnDeath += Die;
    }

    private void Die(HealthModule health)
    {
        _dropSpawner.Spawn(_experience.Level, _transform.position, _dropConfigs);
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}