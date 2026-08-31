using System;
using _Game.Scripts.GamePlay.Experience.Types;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience
{
public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, PlayerModel playerModel)
    {
        return config.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(playerModel.Defense, config.Amount),
            ExperienceType.EntityDiscover => new EntitiesDiscovering(playerModel.Vision, config.Amount),
            ExperienceType.FoodEating => new FoodEating(playerModel.Stomach, config.Amount),
            ExperienceType.DamageResistance => new DamageResisting(playerModel.Defense, config.Amount),
            ExperienceType.DamageTaking => new DamageTaking(playerModel.Health, config.Amount),
            ExperienceType.Healing => new Healing(playerModel.Health, config.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel.Movement, config.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(playerModel.Endurance, config.Amount),
            ExperienceType.DamageDealing => new DamageDealing(playerModel.Attack, config.Amount),
            ExperienceType.StartSprinting => new StartSprinting(playerModel.Movement, config.Amount),
            ExperienceType.DamageBlocking => new DamageBlocking(playerModel.Defense, config.Amount),
            ExperienceType.UnnoticedStaying => new UnnoticedStaying(playerModel.Disguise, config.Amount),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}