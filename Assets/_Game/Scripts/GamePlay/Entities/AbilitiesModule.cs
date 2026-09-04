using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class AbilitiesModule: IStartable, IDisposable
{
    public event Action<AbilityConfig> OnAbilityAdded;
    
    private readonly HashSet<Ability> _abilities = new();
    [Inject] private AbilityFactory _factory;
    [Inject] private EntityModel _entityModel;
    [Inject] private Ticker _ticker;

    public void Start()
    {
        _ticker.OnTick += Tick;
    }
    
    public void Add(AbilityConfig[] configs)
    {
        if (configs == null || configs.Length == 0) return;
        foreach (var ability in configs)
        {
            if (_abilities.Add(_factory.Get(_entityModel, ability)))
            {
                OnAbilityAdded?.Invoke(ability);
            }
        }
    }

    private void Tick(float deltaTime)
    {
        foreach (var ability in _abilities)
        {
            ability.Update(deltaTime);
        }
    }

    public void Dispose()
    {
        _ticker.OnTick -= Tick;
        foreach (var ability in _abilities)
        {
            ability.Dispose();
        }
    }
}
}