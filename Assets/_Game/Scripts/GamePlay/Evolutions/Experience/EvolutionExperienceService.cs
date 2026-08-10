using System;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience
{
public abstract class EvolutionExperienceService
{
    protected readonly PlayerModel PlayerModel;

    public event Action<int> OnExperienceGained;

    private readonly float _maxAmount;
    private float _currentAmount;
    
    protected EvolutionExperienceService(PlayerModel playerModel, float amount)
    {
        PlayerModel = playerModel;
        _maxAmount = amount;
    }

    protected void AddAmount(float amount)
    {
        _currentAmount += amount;
        if (_currentAmount >= _maxAmount)
        {
            OnExperienceGained?.Invoke(1);
        }
    }

    public abstract void Dispose();
}
}