using System;
using _Game.Scripts.Evolutions.Experience.Types;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Evolutions.Experience
{
public class EvolutionExperienceFactory
{
    public IEvolutionExperience GetMethod(EvolutionExperienceType experienceType, PlayerModel playerModel)
    {
        return experienceType switch
        {
            EvolutionExperienceType.DamageReflection => new DamageReflecting(playerModel),
            EvolutionExperienceType.ObjectDiscover => new ObjectsDiscovering(playerModel),
            EvolutionExperienceType.FoodEating => new FoodEating(playerModel),
            _ => throw new ArgumentOutOfRangeException(nameof(experienceType), experienceType, null)
        };
    }
}
}