using System;
using _Game.Scripts.GamePlay.Abilities.Types;
using _Game.Scripts.GamePlay.Entities;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityType
{
    Sprint = 0,
    Dash = 1,
}

public class AbilityFactory
{
    public Ability Get(EntityModel model, AbilityConfig config)
    {
        return config.Type switch
        {
            AbilityType.Sprint => new SprintAbility(model.Movement, model.Endurance, config),
            AbilityType.Dash => new DashAbility(model.Movement, model.Endurance, config),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, null)
        };
    }
}
}