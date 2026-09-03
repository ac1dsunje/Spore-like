using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityBasicAttackBehaviour : IDamageSource, IAttackController, IDamageSourceController
{
    [Inject] private AttackModule _module;
    private IDamageReceiver _receiver;

    public void SetDamageReceiver(IDamageReceiver damageReceiver) => _receiver = damageReceiver;

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 mousePosition)
    {
        damageReceiver.TakeDamage(new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, this, _receiver));
    }

    public void SetDamageDealt(float damage)
    {
        _module.SetDamageDealt(damage);
    }
}
}