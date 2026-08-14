using System;

namespace _Game.Scripts.GamePlay.Experience
{
public abstract class ExperienceService
{
    public event Action<int> OnExperienceGained;

    private readonly float _maxAmount;
    private float _currentAmount;
    
    protected ExperienceService(float amount)
    {
        _maxAmount = amount;
    }

    protected void AddAmount(float amount)
    {
        _currentAmount += amount;
        while (_currentAmount >= _maxAmount)
        {
            OnExperienceGained?.Invoke(1);
            _currentAmount -= _maxAmount;
        }
        
    }

    public abstract void Dispose();
}
}