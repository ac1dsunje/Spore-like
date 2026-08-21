using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageResisting: ExperienceService
{
    private readonly DefenseModule _module;
    
    public DamageResisting(DefenseModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDamageResisted += AddAmount;
    }

    public override void Dispose() => _module.OnDamageResisted -= AddAmount;
}
}