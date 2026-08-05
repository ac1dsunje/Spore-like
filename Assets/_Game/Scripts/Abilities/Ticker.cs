using System;
using UnityEngine;

namespace _Game.Scripts.Abilities
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