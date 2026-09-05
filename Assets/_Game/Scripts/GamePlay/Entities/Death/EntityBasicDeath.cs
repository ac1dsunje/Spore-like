using System;
using _Game.Scripts.GamePlay.Entities.Drops;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Death
{
public class EntityBasicDeath: IStartable, IDisposable
{
    [Inject] private EntitiesRegistry _entitiesRegistry;
    [Inject] private ExperienceModule _experience;
    [Inject] private HealthModule _health;
    [Inject] private DropSpawner _dropSpawner;
    [Inject] private DropsConfig _dropConfigs;
    [Inject] private MovementModule _movement;

    public void Start()
    {
        _health.OnDeath += Die;
    }

    private void Die(HealthModule health)
    {
        _dropSpawner.Spawn(_experience.Level, _movement.Transform.position, _dropConfigs);
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}