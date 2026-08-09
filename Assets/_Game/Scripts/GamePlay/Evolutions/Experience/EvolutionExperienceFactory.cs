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
    DistanceOvercoming = 6
}
public class EvolutionExperienceFactory
{
    public EvolutionExperienceService GetMethod(EvolutionExperienceType experienceType, PlayerModel playerModel)
    {
        return experienceType switch
        {
            EvolutionExperienceType.DamageReflection => new DamageReflecting(playerModel),
            EvolutionExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel),
            EvolutionExperienceType.FoodEating => new FoodEating(playerModel),
            EvolutionExperienceType.DamageResistance => new DamageResisting(playerModel),
            EvolutionExperienceType.DamageTaking => new DamageTaking(playerModel),
            EvolutionExperienceType.Healing => new Healing(playerModel),
            EvolutionExperienceType.DistanceOvercoming => new DistanceOvercoming(playerModel),
            _ => throw new ArgumentOutOfRangeException(nameof(experienceType), experienceType, null)
        };
    }
}
}