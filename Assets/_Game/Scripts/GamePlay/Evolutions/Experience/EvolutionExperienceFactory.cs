using System;
using _Game.Scripts.GamePlay.Evolutions.Experience.Types;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience
{
public enum EvolutionExperienceType
{
    ObjectDiscover = 0,
    FoodEating = 1,
    DamageReflection = 2,
    DamageResistance = 3,
    DamageTaking = 4,
    Healing = 5,
    DistanceOvercoming = 6,
    EnduranceRecovering = 7
}
public class EvolutionExperienceFactory
{
    public EvolutionExperienceService GetMethod(EvolutionExperienceConfig config, PlayerModel playerModel)
    {
        return config.Type switch
        {
            EvolutionExperienceType.DamageReflection => new DamageReflecting(playerModel, config.Amount),
            EvolutionExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel, config.Amount),
            EvolutionExperienceType.FoodEating => new FoodEating(playerModel, config.Amount),
            EvolutionExperienceType.DamageResistance => new DamageResisting(playerModel, config.Amount),
            EvolutionExperienceType.DamageTaking => new DamageTaking(playerModel, config.Amount),
            EvolutionExperienceType.Healing => new Healing(playerModel, config.Amount),
            EvolutionExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel, config.Amount),
            EvolutionExperienceType.EnduranceRecovering => new EnduranceRecovering(playerModel, config.Amount),
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}