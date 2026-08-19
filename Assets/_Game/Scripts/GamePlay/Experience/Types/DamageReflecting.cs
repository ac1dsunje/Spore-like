using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageReflecting: ExperienceService
{
    private readonly DefenseModule _module;
    
    public DamageReflecting(DefenseModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDamageReflected += AddAmount;
    }

    public override void Dispose() => _module.OnDamageReflected -= AddAmount;
}
}