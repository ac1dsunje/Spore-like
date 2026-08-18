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
    DamageDealing = 9,
    StartSprinting = 10,
    DamageBlocking = 11,
    XRayDiscovering = 12,
    SlowEnemiesDamageDealing = 13,
    CaughtEnemiesDamageDealing = 14,
    UnnoticedStaying = 15,
    UnnoticedStayingInRest = 16,
    NoiseTargetDiscovering = 17,
    
}
public class ExperienceFactory
{
    public ExperienceService GetService(ExperienceServiceConfig config, PlayerModel playerModel)
    {
        return config.Type switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(playerModel.Defense, config.Amount),
            ExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel.Vision, config.Amount),
            ExperienceType.FoodEating => new FoodEating(playerModel.MouthModule, config.Amount),
            ExperienceType.DamageResistance => new DamageResisting(playerModel.Defense, config.Amount),
            ExperienceType.DamageTaking => new DamageTaking(playerModel.Health, config.Amount),
            ExperienceType.Healing => new Healing(playerModel.Health, config.Amount),
            ExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel.Movement, config.Amount),
            ExperienceType.EnduranceRecovering => new EnduranceRecovering(playerModel.Endurance, config.Amount),
            ExperienceType.DisguiseObjectFound => new DisguiseObjectFound(playerModel.Vision, config.Amount),
            ExperienceType.DamageDealing => new DamageDealing(playerModel.Attack, config.Amount),
            ExperienceType.StartSprinting => new StartSprinting(playerModel.Movement, config.Amount),
            ExperienceType.DamageBlocking => new DamageBlocking(playerModel.Defense, config.Amount),
            ExperienceType.XRayDiscovering => new XRayDiscovering(playerModel.Vision, config.Amount),
            
            // ExperienceType.SlowEnemiesDamageDealing => 
            // ExperienceType.CaughtEnemiesDamageDealing => 
            // ExperienceType.UnnoticedStaying => 
            // ExperienceType.UnnoticedStayingInRest => 
            // ExperienceType.NoiseTargetDiscovering => 
            
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };
    }
}
}