using System;
using _Game.Scripts.GamePlay.Modules.Endurance;
using R3;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class EnduranceRecovering : ExperienceService
{
    private readonly IDisposable _subscription;
    
    public EnduranceRecovering(EnduranceModule module, float amount) : base(amount)
    {
        _subscription = module.Current
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Current - pair.Previous;
                if (delta > 0)
                {
                    AddAmount(delta);
                }
            });
    }

    public override void Dispose() => _subscription?.Dispose();
}
}