using System;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.Stats;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Vision
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<GameObject> OnGameObjectDiscovered;

    public VisionModule(PlayerStats playerStats): base(playerStats) {}

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.VisionRadius:
                UpdateRadius(value);
                break;
        }
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