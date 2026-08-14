using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DistanceOvercoming: ExperienceService
{
    private readonly MovementModule _module;
    
    public DistanceOvercoming(MovementModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDistanceOvercome += AddAmount;
    }

    public override void Dispose() => _module.OnDistanceOvercome -= AddAmount;
}
}