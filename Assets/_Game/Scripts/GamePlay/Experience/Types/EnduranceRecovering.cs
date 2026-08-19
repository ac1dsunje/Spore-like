using _Game.Scripts.GamePlay.Entity.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class EnduranceRecovering: ExperienceService
{
    private readonly EnduranceModule _module;
    
    public EnduranceRecovering(EnduranceModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnEnduranceRecovered += AddAmount;
    }

    public override void Dispose() => _module.OnEnduranceRecovered -= AddAmount;
}
}