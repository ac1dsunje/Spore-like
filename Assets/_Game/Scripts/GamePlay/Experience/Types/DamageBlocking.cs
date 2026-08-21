using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageBlocking: ExperienceService
{
    private readonly DefenseModule _module;
    
    public DamageBlocking(DefenseModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDamageBlocked += OnDamageBlocked;
    }

    private void OnDamageBlocked() => AddAmount(1);

    public override void Dispose() => _module.OnDamageBlocked -= OnDamageBlocked;
}
}