using _Game.Scripts.GamePlay.Modules;
using R3;
using System;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DistanceOvercoming : ExperienceService
{
    private readonly IDisposable _subscription;
    
    public DistanceOvercoming(MovementModule module, float amount) : base(amount)
    {
        _subscription = module.TotalDistance
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