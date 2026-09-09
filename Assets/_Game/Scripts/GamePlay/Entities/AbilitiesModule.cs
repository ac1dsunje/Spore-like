using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using UnityEngine;
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
            HandleInput(ability);
            ability.Update(deltaTime);
        }
    }

    private void HandleInput(Ability ability)
    {
        var config = ability.Config;
        var isKeyDown = Input.GetKeyDown(config.Key);
        var isKeyUp = Input.GetKeyUp(config.Key);

        switch (config.ActivationType)
        {
            case AbilityActivationType.Pressing:
                if (isKeyDown && !ability.IsActive)
                {
                    ability.TryActivate();
                }
                else if (isKeyUp && ability.IsActive)
                {
                    ability.TryDeactivate();
                }
                break;

            case AbilityActivationType.Toggle:
                if (isKeyDown)
                {
                    if (ability.IsActive)
                        ability.TryDeactivate();
                    else
                        ability.TryActivate();
                }
                break;
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