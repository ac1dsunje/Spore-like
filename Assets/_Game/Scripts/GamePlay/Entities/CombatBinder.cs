using _Game.Scripts.GamePlay.Interfaces;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class CombatBinder: IStartable
{
    [Inject] private IDamageReceiver _damageReceiver;
    [Inject] private IDamageSource _damageSource;
    [Inject] private IDamageReceiverController _damageReceiverController;
    [Inject] private IDamageSourceController _damageSourceController;
    public void Start()
    {
        _damageReceiverController.SetDamageSource(_damageSource);
        _damageSourceController.SetDamageReceiver(_damageReceiver);
    }
}

public interface IDamageReceiverController
{
    public void SetDamageSource(IDamageSource damageSource);
}

public interface IDamageSourceController
{
    public void SetDamageReceiver(IDamageReceiver damageReceiver);
}
}