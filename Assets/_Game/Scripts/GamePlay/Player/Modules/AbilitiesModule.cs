using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Abilities;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public class AbilitiesModule: IDisposable
{
    public event Action<AbilityConfig> OnAbilityAdded;
    
    private readonly HashSet<Ability> _abilities = new();
    private readonly AbilityFactory _factory;
    private EntityModel _entityModel;

    [Inject]
    public AbilitiesModule(AbilityFactory factory)
    {
        _factory = factory;
    }
    
    public void SetModel(EntityModel entityModel)
    {
        _entityModel = entityModel;
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

    public void Dispose()
    {
        foreach (var ability in _abilities)
        {
            ability.Dispose();
        }
    }
}
}