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
    public ExperienceService GetMethod(ExperienceServiceConfig serviceConfig, PlayerModel playerModel)
    {
        return serviceConfig.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(playerModel, serviceConfig.Amount),
            ExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel, serviceConfig.Amount),
            ExperienceType.FoodEating => new FoodEating(playerModel, serviceConfig.Amount),
            ExperienceType.DamageResistance => new DamageResisting(playerModel, serviceConfig.Amount),
            ExperienceType.DamageTaking => new DamageTaking(playerModel, serviceConfig.Amount),
            ExperienceType.Healing => new Healing(playerModel, serviceConfig.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel, serviceConfig.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(playerModel, serviceConfig.Amount),
            ExperienceType.DisguiseObjectFound => new DisguiseObjectFound(playerModel, serviceConfig.Amount),
            _ => throw new ArgumentOutOfRangeException(nameof(serviceConfig), serviceConfig, null)
        };
    }
}
}