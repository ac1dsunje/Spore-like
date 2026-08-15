using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinAttackBehaviour : MonoBehaviour
{
    private AttackModule _module;
    private IDamageAble _owner;

    [Inject]
    private void Construct(AttackModule attackModule)
    {
        _module = attackModule;
    }

    public void SetOwner(IDamageAble damageable) => _owner = damageable;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;

        damageAble.TakeDamage(new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, _owner));
    }
}
}