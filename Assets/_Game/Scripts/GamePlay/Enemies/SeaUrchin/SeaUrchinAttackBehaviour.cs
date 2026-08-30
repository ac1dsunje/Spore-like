using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinAttackBehaviour : MonoBehaviour, IDamageSource, IDamageSourceController
{
    private AttackModule _module;
    private IDamageReceiver _receiver;

    [Inject]
    private void Construct(AttackModule attackModule)
    {
        _module = attackModule;
    }

    public void SetDamageReceiver(IDamageReceiver damageReceiver) => _receiver = damageReceiver;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageReceiver damageAble)) return;

        damageAble.TakeDamage(new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, this, _receiver));
    }

    public void SetDamageDealt(float damage)
    {
        _module.SetDamageDealt(damage);
    }
}
}