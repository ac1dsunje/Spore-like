using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Abilities;

namespace _Game.Scripts.GamePlay.Player.Modules.Abilities
{
public class AbilitiesModule: IDisposable
{
    public event Action<AbilityConfig> OnAbilityAdded;
    
    private readonly HashSet<Ability> _abilities = new();
    private AbilityFactory _factory;

    public void SetFactory(AbilityFactory factory)
    {
        _factory = factory;
    }
    
    public void Add(AbilityConfig[] configs)
    {
        if (configs == null || configs.Length == 0) return;
        foreach (var ability in configs)
        {
            if (_abilities.Add(_factory.Get(ability)))
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