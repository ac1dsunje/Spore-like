using System;
using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience
{
public abstract class EvolutionExperienceService
{
    protected readonly PlayerModel PlayerModel;

    public event Action<int> OnExperienceGained;
    
    protected EvolutionExperienceService(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
    }
    
    protected void RaiseEvent(int experiencePoints) => OnExperienceGained?.Invoke(experiencePoints);

    public abstract void Dispose();
}
}