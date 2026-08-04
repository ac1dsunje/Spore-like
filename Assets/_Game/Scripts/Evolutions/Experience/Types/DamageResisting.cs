using System;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class DamageResisting: IEvolutionExperience
{
    private readonly PlayerModel _playerModel;

    public event Action<int> OnExperienceGained;
    
    public DamageResisting(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.Defense.OnDamageResisted += OnDamageResisted;
    }
    
    private void OnDamageResisted(int damage) => OnExperienceGained?.Invoke(damage);

    public void Dispose() => _playerModel.Defense.OnDamageResisted -= OnDamageResisted;
}
}