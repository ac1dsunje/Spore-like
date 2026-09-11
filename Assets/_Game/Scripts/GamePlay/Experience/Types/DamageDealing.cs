using System;
using _Game.Scripts.GamePlay.Modules;
using R3;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageDealing : ExperienceService
{
    private readonly IDisposable _subscription;
    
    public DamageDealing(AttackModule module, float amount) : base(amount)
    {
        _subscription = module.Damaged
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Current - pair.Previous;
                if (delta > 0) AddAmount(delta);
            });
    }

    public override void Dispose() => _subscription?.Dispose();
}
}