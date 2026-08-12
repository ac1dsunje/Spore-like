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
    private PlayerModel _playerModel;

    public void Initialize(AbilityFactory factory, PlayerModel playerModel)
    {
        _factory = factory;
        _playerModel = playerModel;
    }
    
    public void Add(AbilityConfig[] configs)
    {
        if (configs == null || configs.Length == 0) return;
        foreach (var ability in configs)
        {
            if (_abilities.Add(_factory.Get(_playerModel, ability)))
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