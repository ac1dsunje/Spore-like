using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityEmptyAttack : IDamageSource, IAttackController
{
    [Inject] private AttackModule _module;
    [Inject] private IDamageReceiver _receiver;

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 targetPosition)
    {
        
    }

    public void SetDamageDealt(float damage)
    {
        _module.SetDamageDealt(damage);
    }
}
}