using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class OnHitAttackBehaviour : IStartable, IDamageSource, IDamageSourceController
{
    [Inject] private AttackModule _module;
    [Inject] private BodyHitbox _hitbox;
    private IDamageReceiver _receiver;

    public void SetDamageReceiver(IDamageReceiver damageReceiver) => _receiver = damageReceiver;

    public void Start()
    {
        _hitbox.OnDamageReceiver += DoDamage;
    }

    private void DoDamage(IDamageReceiver damageReceiver)
    {
        damageReceiver.TakeDamage(new HitInfo(_module.PhysicalDamage, _module.IgnoreResistance, this, _receiver));
    }

    public void SetDamageDealt(float damage)
    {
        _module.SetDamageDealt(damage);
    }
}
}