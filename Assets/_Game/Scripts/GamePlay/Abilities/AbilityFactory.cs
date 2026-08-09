using System;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities.Types;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityType
{
    Sprint,
    Dash,
}

public class AbilityFactory
{
    private readonly PlayerModel _model;
    private readonly Ticker _ticker;
    
    public AbilityFactory(PlayerModel model, Ticker ticker)
    {
        _model = model;
        _ticker = ticker;
    }

    public Ability Get(AbilityConfig config)
    {
        return config.Type switch
        {
            AbilityType.Sprint => new SprintAbility(_model, config, _ticker),
            AbilityType.Dash => new DashAbility(_model, config, _ticker),
            _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, null)
        };
    }
}
}