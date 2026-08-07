using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Vision
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<GameObject> OnGameObjectDiscovered;

    public VisionModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.VisionRadius, UpdateRadius);
    }
    
    private void UpdateRadius(float newRadius)
    {
        VisionRadius = newRadius;
        OnVisionRadiusChanged?.Invoke(VisionRadius);
    }

    public void DiscoverGameObject(GameObject gameObject)
    {
        OnGameObjectDiscovered?.Invoke(gameObject);
    }
}
}