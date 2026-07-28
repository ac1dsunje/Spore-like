using System;
using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class DamageReflecting: IEvolutionExperience
{
    private readonly PlayerStats _playerStats;

    public event Action<int> OnExperienceGained;
    public DamageReflecting(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        _playerStats.Attack.OnDamageReflected += OnDamageReflected;
    }
    
    private void OnDamageReflected(int damage)
    {
        OnExperienceGained?.Invoke(damage);
    }

    public void Dispose()
    {
        _playerStats.Attack.OnDamageReflected -= OnDamageReflected;
    }
}
}