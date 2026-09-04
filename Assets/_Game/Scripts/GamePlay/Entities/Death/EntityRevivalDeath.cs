using System;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Death
{
public class EntityRevivalDeath: IStartable, IDisposable
{
    [Inject] private EntitiesRegistry _entitiesRegistry;
    [Inject] private ExperienceModule _experience;
    [Inject] private HealthModule _health;

    public void Start()
    {
        _health.OnDeath += Die;
    }

    private void Die(HealthModule health)
    {
        //TODO: do something
    }
    
    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}