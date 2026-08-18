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
    XRay = 3,
    SlowEnemies = 4,
    Catch = 5,
    ColorChange = 6,
    NoiseLabels = 7,
    
}

public class AbilityFactory
{
    private readonly Ticker _ticker;
    private readonly IInputService _input;
    
    [Inject]
    public AbilityFactory(Ticker ticker, IInputService inputService)
    {
        _ticker = ticker;
        _input = inputService;
    }

    public Ability Get(PlayerModel model, AbilityConfig config)
    {
        return config.Type switch
        {
            AbilityType.Sprint => new SprintAbility(model.Movement, model.Endurance, config, _ticker, _input),
            AbilityType.Dash => new DashAbility(model.Movement, model.Endurance, config, _ticker, _input),
            AbilityType.Light => new LightAbility(model.Vision, model.Endurance, config, _ticker, _input),
            AbilityType.XRay => new XRayAbility(model.Vision, model.Endurance, config, _ticker, _input),
            
            //AbilityType.SlowEnemies => 
            //AbilityType.Catch => 
            //AbilityType.ColorChange => 
            //AbilityType.NoiseLabels => 
            
            _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, null)
        };
    }
}
}