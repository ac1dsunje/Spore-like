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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageReceiver damageAble)) return;

        damageAble.TakeDamage(new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, this, _receiver));
    }

    public void SetDamageDealt(float damage)
    {
        _module.SetDamageDealt(damage);
    }
}
}