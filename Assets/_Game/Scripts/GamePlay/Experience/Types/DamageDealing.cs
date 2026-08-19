using _Game.Scripts.GamePlay.Entity.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageDealing: ExperienceService
{
    private readonly AttackModule _module;
    
    public DamageDealing(AttackModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDamageDealt += AddAmount;
    }

    public override void Dispose() => _module.OnDamageDealt -= AddAmount;
}
}