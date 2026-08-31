using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class VisionModule: StatModule
{
    public float VisionRadius { get; private set; }

    private float _sensorics;
    private float _lightingRadius;

    private bool _useLight;

    private readonly HashSet<IVisible> _objectsInVision = new();

    public event Action<IVisible, bool> OnEntityDiscovered;

    public event Action<float> OnVisionRadiusUpdated;
    public event Action<float, bool> OnLightingUpdated;

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

    private void UpdateSensorics(float value)
    {
        _sensorics = value;

        foreach (var visible in _objectsInVision)
        {
            TryDiscoverEntity(visible);
        }
    }

    public void EnterEntity(IVisible visible)
    {
        if (!_objectsInVision.Add(visible)) return;

        TryDiscoverEntity(visible);
    }

    public void ExitObject(IVisible visible) => _objectsInVision.Remove(visible);

    private void TryDiscoverEntity(IVisible visible)
    {
        OnEntityDiscovered?.Invoke(visible, visible.IsDetected(_sensorics));
    }
}
}