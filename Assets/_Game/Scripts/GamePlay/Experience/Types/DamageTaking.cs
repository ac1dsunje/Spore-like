using System;
using _Game.Scripts.GamePlay.Modules.Health;
using R3;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageTaking : ExperienceService
{
    private readonly IDisposable _subscription;
    
    public DamageTaking(HealthModule module, float amount) : base(amount)
    {
        _subscription = module.Current
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Previous - pair.Current;
                if (delta > 0)
                {
                    AddAmount(delta);
                }
            });
    }

    public override void Dispose() => _subscription?.Dispose();
}
}