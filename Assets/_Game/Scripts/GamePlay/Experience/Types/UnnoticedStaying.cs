using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class UnnoticedStaying: ExperienceService
{
    private readonly DisguiseModule _module;
    
    public UnnoticedStaying(DisguiseModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnUnnoticed += OnUnnoticed;
    }

    private void OnUnnoticed() => AddAmount(1);

    public override void Dispose() => _module.OnUnnoticed -= OnUnnoticed;
}
}