using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class Ticker: MonoBehaviour
{
    public event Action<float> OnTick;
    
    private void Update()
    {
        OnTick?.Invoke(Time.deltaTime);
    }
}
}