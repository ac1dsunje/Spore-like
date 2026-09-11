using System;
using _Game.Scripts.GamePlay.Abilities.Types;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Endurance;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityType
{
    Sprint = 0,
    Dash = 1,
}

public class AbilityFactory
{
    public Ability Get(EntityScope entity, AbilityConfig config)
    {
        return config.Type switch
        {
            AbilityType.Sprint => new SprintAbility(
                entity.Get<MovementModule>(),
                entity.Get<EnduranceModule>(),
                entity.Get<EnduranceRecoveryModule>(),
                config),
            AbilityType.Dash => new DashAbility(
                entity.Get<MovementModule>(),
                entity.Get<EnduranceModule>(),
                entity.Get<EnduranceRecoveryModule>(),
                config),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config.Type), config.Type, null)
        };
    }
}
}