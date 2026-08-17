using System;
using System.Collections.Generic;
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

    private readonly HashSet<GameObject> _objectsInVision = new();

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
        OnXRayUpdated?.Invoke(_xRay, _useXRay);
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

    private void UpdateSensorics(float value)
    {
        _sensorics = value;
        foreach (var gameObject in _objectsInVision)
        {
            TryDiscoverObject(gameObject);
        }
    }

    private void UpdateXRayRadius(float value)
    {
        _xRay = value;
        OnXRayUpdated?.Invoke(_xRay, _useXRay);
    }

    public void EnterObject(GameObject gameObject)
    {
        if (!_objectsInVision.Add(gameObject)) return;
        TryDiscoverObject(gameObject);
    }

    private void TryDiscoverObject(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IDisguiseAble disguiseAble))
        {
            if (disguiseAble.SetVisible(_sensorics))
            {
                OnDisguiseAbleDiscovered?.Invoke(disguiseAble);
                OnGameObjectDiscovered?.Invoke(gameObject);
            }
            return;
        }
        OnGameObjectDiscovered?.Invoke(gameObject);
    }

    public void ExitObject(GameObject gameObject)
    {
        _objectsInVision.Remove(gameObject);
    }
}
}