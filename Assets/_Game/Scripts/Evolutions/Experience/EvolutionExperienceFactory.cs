using System;
using _Game.Scripts.Evolutions.Experience.Types;
using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience
{
public class EvolutionExperienceFactory
{
    public IEvolutionExperience GetMethod(EvolutionExperienceType experienceType, PlayerStats playerStats)
    {
        return experienceType switch
        {
            EvolutionExperienceType.DamageReflection => new DamageReflecting(playerStats),
            EvolutionExperienceType.ObjectDiscover => new ObjectsDiscovering(playerStats),
            _ => throw new ArgumentOutOfRangeException(nameof(experienceType), experienceType, null)
        };
    }
}
}