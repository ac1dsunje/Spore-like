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
    private float _xRay;

    public event Action<GameObject> OnGameObjectDiscovered;
    public event Action<IDisguiseAble> OnDisguiseAbleDiscovered;
    public event Action<float> OnVisionRadiusUpdated;
    public event Action<float, bool> OnLightingUpdated;
    public event Action<float, bool> OnXRayUpdated;

    private bool _useLight;
    private bool _useXRay;

    protected override void Configure()
    {
        BindStat(StatType.VisionRadius, UpdateVisionRadius);
        BindStat(StatType.Sensorics, UpdateSensorics);
        BindStat(StatType.LightingRadius, UpdateLightingRadius);
        BindStat(StatType.XRay, UpdateXRayRadius);
    }

    public void SetLight(bool state)
    {
        _useLight = state;
        OnLightingUpdated?.Invoke(_lightingRadius, _useLight);
    }

    public void SetXRay(bool state)
    {
        _useXRay = state;
        OnXRayUpdated?.Invoke(_lightingRadius, _useXRay);
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
    private void UpdateXRayRadius(float value)
    {
        _xRay = value;
        OnXRayUpdated?.Invoke(_xRay, _useXRay);
    }

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