using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class AbilitiesModule: IStartable, IDisposable
{
    public event Action<AbilityConfig> OnAbilityAdded;
    
    private readonly HashSet<Ability> _abilities = new();
    private readonly AbilityFactory _factory;
    private readonly EntityModel _entityModel;
    private readonly Ticker _ticker;

    public AbilitiesModule(Ticker ticker, AbilityFactory factory, EntityModel entityModel)
    {
        _ticker = ticker;
        _factory = factory;
        _entityModel = entityModel;
    }

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