using System;

namespace _Game.Scripts.Evolutions.Experience
{
public interface IEvolutionExperience: IDisposable
{
    public event Action<int> OnExperienceGained;
}
}