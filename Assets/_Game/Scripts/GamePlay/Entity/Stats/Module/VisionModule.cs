using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entity.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entity.Module
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }
    public float XRayRadius { get; private set; }

    private float _sensorics;
    private float _lightingRadius;

    private bool _useLight;
    private bool _useXRay;

    private readonly HashSet<GameObject> _objectsInVision = new();
    private readonly HashSet<GameObject> _objectsInXRay = new();

    public event Action<GameObject> OnGameObjectDiscovered;
    public event Action<IDisguisable> OnDisguiseAbleDiscovered;
    public event Action<IDisguisable> OnDiscoveredWithXRay;

    public event Action<float> OnVisionRadiusUpdated;
    public event Action<float, bool> OnLightingUpdated;
    public event Action<float, bool> OnXRayUpdated;

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

        if (!state) _objectsInXRay.Clear();

        OnXRayUpdated?.Invoke(XRayRadius, _useXRay);

        RefreshVisibility();
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
            TryDiscoverObjectWithSensorics(gameObject);
        }
    }

    private void UpdateXRayRadius(float value)
    {
        XRayRadius = value;
        OnXRayUpdated?.Invoke(XRayRadius, _useXRay);
    }

    public void EnterObject(GameObject gameObject)
    {
        if (!_objectsInVision.Add(gameObject)) return;

        TryDiscoverObjectWithSensorics(gameObject);
    }

    public void ExitObject(GameObject gameObject)
    {
        _objectsInVision.Remove(gameObject);
    }

    public void EnterXRay(GameObject gameObject)
    {
        if (!_useXRay) return;

        if (!_objectsInXRay.Add(gameObject)) return;

        TryDiscoverObjectWithXRay(gameObject);
    }

    public void ExitXRay(GameObject gameObject)
    {
        _objectsInXRay.Remove(gameObject);

        if (_objectsInVision.Contains(gameObject))
        {
            TryDiscoverObjectWithSensorics(gameObject);
        }
    }

    private void TryDiscoverObjectWithSensorics(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IDisguisable disguiseAble))
        {
            if (disguiseAble.IsDetected(_sensorics, false))
            {
                OnDisguiseAbleDiscovered?.Invoke(disguiseAble);
                OnGameObjectDiscovered?.Invoke(gameObject);
            }
            return;
        }
        OnGameObjectDiscovered?.Invoke(gameObject);
    }

    private void TryDiscoverObjectWithXRay(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IDisguisable disguiseAble))
        {
            if (disguiseAble.IsDetected(0f, true))
            {
                OnDisguiseAbleDiscovered?.Invoke(disguiseAble);
                OnDiscoveredWithXRay?.Invoke(disguiseAble);
            }
        }

        OnGameObjectDiscovered?.Invoke(gameObject);
    }

    private void RefreshVisibility()
    {
        foreach (var gameObject in _objectsInXRay)
        {
            if (!_useXRay) break;

            TryDiscoverObjectWithXRay(gameObject);
        }

        foreach (var gameObject in _objectsInVision)
        {
            TryDiscoverObjectWithSensorics(gameObject);
        }
    }
}
}