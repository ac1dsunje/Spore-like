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
        while (_current >= _max)
        {
            OnExperienceGained?.Invoke(1);
            _current -= _max;
        }
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
}
