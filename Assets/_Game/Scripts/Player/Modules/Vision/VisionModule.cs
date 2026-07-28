using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Vision
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }
    public float SensoricsRadius { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<GameObject> OnGameObjectDiscovered;

    public VisionModule(PlayerStats stats): base(stats) {}

    protected override void OnStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.VisionRadius:
                UpdateRadius(value);
                break;
            case StatType.SensoricsRadius:
                UpdateSensoricsRadius(value);
                break;
        }
    }
    
    private void UpdateRadius(float newRadius)
    {
        VisionRadius = newRadius;
        OnVisionRadiusChanged?.Invoke(VisionRadius);
    }

    private void UpdateSensoricsRadius(float newRadius) => SensoricsRadius = newRadius;

    public void DiscoverGameObject(GameObject gameObject)
    {
        OnGameObjectDiscovered?.Invoke(gameObject);
    }
}
}