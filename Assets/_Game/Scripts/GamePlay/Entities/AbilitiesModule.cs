using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Abilities;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class AbilitiesModule: IDisposable
{
    public event Action<AbilityConfig> OnAbilityAdded;
    
    private readonly HashSet<Ability> _abilities = new();
    [Inject] private AbilityFactory _factory;
    [Inject] private EntityModel _entityModel;
    
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

    public void Dispose()
    {
        foreach (var ability in _abilities)
        {
            ability.Dispose();
        }
    }
}
}