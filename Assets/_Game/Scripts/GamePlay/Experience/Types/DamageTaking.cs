using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageTaking: ExperienceService
{
    private readonly HealthModule _module;
    
    public DamageTaking(HealthModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDamageTaken += AddAmount;
    }

    public override void Dispose() => _module.OnDamageTaken -= AddAmount;
}
}