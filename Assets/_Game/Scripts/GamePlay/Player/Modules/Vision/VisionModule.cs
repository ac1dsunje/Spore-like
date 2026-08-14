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
    public float LightingRadius => _useLight ? _lightingRadius : 0f;
    
    private float _lightingRadius;

    private bool _useLight;

    public event Action<GameObject> OnGameObjectDiscovered;

    [Inject]
    public VisionModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.VisionRadius, UpdateVisionRadius);
        BindStat(StatType.Sensorics, UpdateSensorics);
        BindStat(StatType.LightingRadius, UpdateLightingRadius);
    }

    public void RequestLight()
    {
        _useLight = true;
    }

    public void ResetLight()
    {
        _useLight = false;
    }
    
    private void UpdateVisionRadius(float value) => VisionRadius = value;

    private void UpdateLightingRadius(float value) => _lightingRadius = value;

    private void UpdateSensorics(float value) => Sensorics = value;

    public void DiscoverGameObject(GameObject gameObject)
    {
        OnGameObjectDiscovered?.Invoke(gameObject);
    }
}
}