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
    [Inject] private FoodDropper _foodDropper;

    public void Start()
    {
        _health.OnDeath += Die;
    }

    private void Die(HealthModule health)
    {
        _foodDropper.Spawn(_experience.Level);
        _entitiesRegistry.DestroyEntityByHealth(health);
    }
    
    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}