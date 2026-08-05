using System;
using System.Collections.Generic;
using _Game.Scripts.Abilities;

namespace _Game.Scripts.Player.Modules.Abilities
{
public class AbilitiesModule: IDisposable
{
    private readonly List<Ability> _abilities = new();
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
            _abilities.Add(_factory.Get(ability));
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