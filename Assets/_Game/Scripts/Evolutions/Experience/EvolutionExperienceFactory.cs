using System;
using _Game.Scripts.Evolutions.Experience.Types;
using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience
{
public class EvolutionExperienceFactory
{
    public IEvolutionExperience GetMethod(ExperienceType experienceType, PlayerStats playerStats)
    {
        return experienceType switch
        {
            ExperienceType.DamageReflection => new DamageReflecting(playerStats),
            ExperienceType.ObjectDiscover => new ObjectsDiscovering(playerStats),
            _ => throw new ArgumentOutOfRangeException(nameof(experienceType), experienceType, null)
        };
    }
}
}