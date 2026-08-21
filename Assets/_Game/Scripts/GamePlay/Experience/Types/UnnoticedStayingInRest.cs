using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class UnnoticedStayingInRest: ExperienceService
{
    private readonly DisguiseModule _module;
    
    public UnnoticedStayingInRest(DisguiseModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnUnnoticedInRest += OnUnnoticed;
    }

    private void OnUnnoticed() => AddAmount(1);

    public override void Dispose() => _module.OnUnnoticedInRest -= OnUnnoticed;
}
}