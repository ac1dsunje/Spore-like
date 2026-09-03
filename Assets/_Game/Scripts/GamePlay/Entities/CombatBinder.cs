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
    public void Start()
    {
        _damageReceiverController.SetDamageSource(_damageSource);
    }
}

public interface IDamageReceiverController
{
    public void SetDamageSource(IDamageSource damageSource);
}
}