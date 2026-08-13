using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Vision
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }
    public float Sensorics { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<GameObject> OnGameObjectDiscovered;

    [Inject]
    public VisionModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.VisionRadius, UpdateRadius);
        BindStat(StatType.Sensorics, UpdateSensorics);
    }
    
    private void UpdateRadius(float value)
    {
        VisionRadius = value;
        OnVisionRadiusChanged?.Invoke(VisionRadius);
    }
    
    private void UpdateSensorics(float value) => Sensorics = value;

    public void DiscoverGameObject(GameObject gameObject)
    {
        OnGameObjectDiscovered?.Invoke(gameObject);
    }
}
}