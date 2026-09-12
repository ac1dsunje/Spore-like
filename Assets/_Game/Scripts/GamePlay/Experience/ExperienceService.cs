using System;
using R3;

namespace _Game.Scripts.GamePlay.Experience
{
public class ExperienceService : IDisposable
{
    public event Action<float> OnExperienceGained;

    private readonly float _max;
    private float _current;
    private readonly IDisposable _subscription;
    
    public ExperienceService(Observable<float> observable, float amount, DeltaDirection direction)
    {
        _max = amount;
            
        _subscription = observable
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Current - pair.Previous;
                    
                var effectiveDelta = direction == DeltaDirection.Increase ? delta : -delta;

                if (effectiveDelta > 0)
                {
                    AddAmount(effectiveDelta);
                }
            });
    }

    private void AddAmount(float amount)
    {
        _current += amount;

        var intAmount = (int)(_current / _max);

        if (intAmount <= 0) return;
        
        OnExperienceGained?.Invoke(intAmount);
        
        _current -= intAmount * _max;
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
}
