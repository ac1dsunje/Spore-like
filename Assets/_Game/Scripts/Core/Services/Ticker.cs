using System;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.Core.Services
{
public class Ticker : ITickable, IFixedTickable
{
    public event Action<float> OnTick;
    public event Action OnFixedTick;
    
    public void Tick()
    {
        OnTick?.Invoke(Time.deltaTime);
    }

    public void FixedTick()
    {
        OnFixedTick?.Invoke();
    }
}
}