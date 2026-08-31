using System;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.Core.Services
{
public class Ticker: ITickable
{
    public event Action<float> OnTick;
    
    public void Tick()
    {
        OnTick?.Invoke(Time.deltaTime);
    }
}
}