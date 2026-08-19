using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class StartSprinting: ExperienceService
{
    private readonly MovementModule _module;
    
    public StartSprinting(MovementModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnSprint += OnSprint;
    }

    private void OnSprint() => AddAmount(1);

    public override void Dispose() => _module.OnSprint -= OnSprint;
}
}