using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Experience.Types;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Endurance;
using _Game.Scripts.GamePlay.Modules.Health;

namespace _Game.Scripts.GamePlay.Experience
{
public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, EntityScope entity)
    {
        return config.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(entity.Get<DefenseModule>(), config.Amount),
            ExperienceType.EntityDiscover => new EntitiesDiscovering(entity.Get<VisionModule>(), config.Amount),
            ExperienceType.FoodEating => new FoodEating(entity.Get<StomachModule>(), config.Amount),
            ExperienceType.DamageResistance => new DamageResisting(entity.Get<DefenseModule>(), config.Amount),
            ExperienceType.DamageTaking => new DamageTaking(entity.Get<HealthModule>(), config.Amount),
            ExperienceType.Healing => new Healing(entity.Get<HealthModule>(), config.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(entity.Get<MovementModule>(), config.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(entity.Get<EnduranceModule>(), config.Amount),
            ExperienceType.DamageDealing => new DamageDealing(entity.Get<AttackModule>(), config.Amount),
            ExperienceType.ExperienceCollecting => new ExperienceCollecting(entity.Get<PickingModule>(), config.Amount),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}