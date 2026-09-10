using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class VisionModule : StatModule
{
    public float VisionRadius { get; private set; }

    private readonly HashSet<IVisible> _objectsInVision = new();

    public event Action<IVisible> OnEntityDiscovered;
    public event Action<float> OnVisionRadiusUpdated;

    protected override void Configure()
    {
        BindStat(StatType.VisionRadius, UpdateVisionRadius);
    }

    public bool CanSee()
    {
        return VisionRadius > 0.1f;
    }

    private void UpdateVisionRadius(float value)
    {
        VisionRadius = value;
        OnVisionRadiusUpdated?.Invoke(VisionRadius);
    }

    public void EnterEntity(IVisible visible)
    {
        if (!_objectsInVision.Add(visible)) return;

        OnEntityDiscovered?.Invoke(visible);
    }

    public void ExitEntity(IVisible visible) => _objectsInVision.Remove(visible);
}
}