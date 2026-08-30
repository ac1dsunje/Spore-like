using _Game.Scripts.GamePlay.Interfaces;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class CombatBinder
{
    [Inject]
    public CombatBinder(IDamageReceiver damageReceiver, IDamageSource damageSource,
        IDamageReceiverController damageReceiverController, IDamageSourceController damageSourceController)
    {
        damageReceiverController.SetDamageSource(damageSource);
        damageSourceController.SetDamageReceiver(damageReceiver);
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