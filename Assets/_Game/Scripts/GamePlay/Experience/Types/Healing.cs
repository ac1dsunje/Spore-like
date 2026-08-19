using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class Healing: ExperienceService
{
    private readonly HealthModule _module;
    
    public Healing(HealthModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnHealed += AddAmount;
    }

    public override void Dispose() => _module.OnHealed -= AddAmount;
}
}