using System;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Module
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }
    private float _sensorics;
    private float _lightingRadius;

    public event Action<GameObject> OnGameObjectDiscovered;
    public event Action<IDisguiseAble> OnDisguiseAbleDiscovered;
    public event Action<float> OnVisionRadiusUpdated;
    public event Action<float, bool> OnLightingUpdated;

    private bool _useLight;

    protected override void Configure()
    {
        BindStat(StatType.VisionRadius, UpdateVisionRadius);
        BindStat(StatType.Sensorics, UpdateSensorics);
        BindStat(StatType.LightingRadius, UpdateLightingRadius);
    }

    public void SetLight(bool state)
    {
        _useLight = state;
        OnLightingUpdated?.Invoke(_lightingRadius, _useLight);
    }

    private void UpdateVisionRadius(float value)
    {
        VisionRadius = value;
        OnVisionRadiusUpdated?.Invoke(VisionRadius);
    }

    private void UpdateLightingRadius(float value)
    {
        _lightingRadius = value;
        OnLightingUpdated?.Invoke(_lightingRadius, _useLight);
    }

    private void UpdateSensorics(float value) => _sensorics = value;

    public void DiscoverGameObject(GameObject gameObject)
    {
        OnGameObjectDiscovered?.Invoke(gameObject);
        if (gameObject.TryGetComponent(out IDisguiseAble disguiseAble))
        {
            if (disguiseAble.SetVisible(_sensorics))
            {
                OnDisguiseAbleDiscovered?.Invoke(disguiseAble);
            }
        }
    }
}
}