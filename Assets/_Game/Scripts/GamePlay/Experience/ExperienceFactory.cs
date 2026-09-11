using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Endurance;
using _Game.Scripts.GamePlay.Modules.Health;

namespace _Game.Scripts.GamePlay.Experience
{
public enum DeltaDirection
{
    Increase,
    Decrease
}

public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, EntityScope entity)
    {
        var (observable, direction) = config.Type switch
        {
            ExperienceType.Healing             => (entity.Get<HealthModule>().Current, DeltaDirection.Increase),
            ExperienceType.DamageTaking        => (entity.Get<HealthModule>().Current, DeltaDirection.Decrease),
                
            ExperienceType.FoodEating          => (entity.Get<StomachModule>().Current, DeltaDirection.Increase),
                
            ExperienceType.DamageReflection    => (entity.Get<DefenseModule>().Reflected, DeltaDirection.Increase), 
            ExperienceType.DamageResistance    => (entity.Get<DefenseModule>().Resisted, DeltaDirection.Increase),
                
            ExperienceType.DistanceOvercoming  => (entity.Get<MovementModule>().TotalDistance, DeltaDirection.Increase),
            ExperienceType.EnduranceRecovering => (entity.Get<EnduranceModule>().Current, DeltaDirection.Increase),
            
            ExperienceType.DamageDealing       => (entity.Get<AttackModule>().Damaged, DeltaDirection.Increase),
            
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };

        return new ExperienceService(observable, config.Amount, direction);
    }
}
}