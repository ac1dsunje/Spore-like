using System;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities.Types;
using _Game.Scripts.GamePlay.Player;
using VContainer;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityType
{
    Sprint = 0,
    Dash = 1,
    Light = 2,
}

public class AbilityFactory
{
    private readonly Ticker _ticker;
    
    [Inject]
    public AbilityFactory(Ticker ticker)
    {
        _ticker = ticker;
    }

    public Ability Get(EntityModel model, AbilityConfig config)
    {
        return config.Type switch
        {
            AbilityType.Sprint => new SprintAbility(model.Movement, model.Endurance, config, _ticker),
            AbilityType.Dash => new DashAbility(model.Movement, model.Endurance, config, _ticker),
            AbilityType.Light => new LightAbility(model.Vision, model.Endurance, config, _ticker),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, null)
        };
    }
}
}