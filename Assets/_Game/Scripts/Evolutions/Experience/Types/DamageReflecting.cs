using System;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class DamageReflecting: IEvolutionExperience
{
    private readonly PlayerModel _playerModel;

    public event Action<int> OnExperienceGained;
    public DamageReflecting(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.Defense.OnDamageReflected += OnDamageReflected;
    }
    
    private void OnDamageReflected(int damage) => OnExperienceGained?.Invoke(damage);

    public void Dispose() => _playerModel.Defense.OnDamageReflected -= OnDamageReflected;
}
}