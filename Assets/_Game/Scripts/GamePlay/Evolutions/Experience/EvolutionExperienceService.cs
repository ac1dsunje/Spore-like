using System;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience
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