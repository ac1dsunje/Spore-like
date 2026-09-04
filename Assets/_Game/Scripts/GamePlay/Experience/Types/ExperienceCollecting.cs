using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class ExperienceCollecting: ExperienceService
{
    private readonly PickingModule _module;
    
    public ExperienceCollecting(PickingModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnExperiencePointCollected += AddAmount;
    }

    public override void Dispose() => _module.OnExperiencePointCollected -= AddAmount;
}
}