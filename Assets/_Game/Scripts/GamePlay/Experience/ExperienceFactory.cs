using System;
using _Game.Scripts.GamePlay.Experience.Types;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience
{
public enum ExperienceType
{
    ObjectDiscover = 0,
    FoodEating = 1,
    DamageReflection = 2,
    DamageResistance = 3,
    DamageTaking = 4,
    Healing = 5,
    DistanceOvercoming = 6,
    EnduranceRecovering = 7,
    DisguiseObjectFound = 8,
}
public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, PlayerModel playerModel)
    {
        return config.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(playerModel, config.Amount),
            ExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel, config.Amount),
            ExperienceType.FoodEating => new FoodEating(playerModel, config.Amount),
            ExperienceType.DamageResistance => new DamageResisting(playerModel, config.Amount),
            ExperienceType.DamageTaking => new DamageTaking(playerModel, config.Amount),
            ExperienceType.Healing => new Healing(playerModel, config.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel, config.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(playerModel, config.Amount),
            ExperienceType.DisguiseObjectFound => new DisguiseObjectFound(playerModel, config.Amount),
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}