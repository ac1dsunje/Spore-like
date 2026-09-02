using System;
using _Game.Scripts.GamePlay.Experience.Types;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience
{
public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, EntityModel entityModel)
    {
        return config.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(entityModel.Defense, config.Amount),
            ExperienceType.EntityDiscover => new EntitiesDiscovering(entityModel.Vision, config.Amount),
            ExperienceType.FoodEating => new FoodEating(entityModel.Stomach, config.Amount),
            ExperienceType.DamageResistance => new DamageResisting(entityModel.Defense, config.Amount),
            ExperienceType.DamageTaking => new DamageTaking(entityModel.Health, config.Amount),
            ExperienceType.Healing => new Healing(entityModel.Health, config.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(entityModel.Movement, config.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(entityModel.Endurance, config.Amount),
            ExperienceType.DamageDealing => new DamageDealing(entityModel.Attack, config.Amount),
            ExperienceType.StartSprinting => new StartSprinting(entityModel.Movement, config.Amount),
            ExperienceType.UnnoticedStaying => new UnnoticedStaying(entityModel.Disguise, config.Amount),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}