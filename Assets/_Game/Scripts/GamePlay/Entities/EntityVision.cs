using System;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityVision : IInitializable, IDisposable
{
    [Inject] private VisionModule _module;
    [Inject] private CameraController _camController;
    [Inject] private VisionHitbox _visionHitbox;

    public void Initialize()
    {
        _module.OnVisionRadiusUpdated += ApplyVision;
        _module.OnEntityDiscovered += RevealEntity;
        
        ApplyVision(_module.VisionRadius);
    }

    private void RevealEntity(IVisible entity, bool state)
    {
        entity.SetVisible(state);
    }

    private void ApplyVision(float value)
    {
        _visionHitbox.SetSize(new Vector2(_camController.Aspect, 1f) * (value * 2f));
    }

    public void Dispose()
    {
        _module.OnVisionRadiusUpdated -= ApplyVision;
        _module.OnEntityDiscovered -= RevealEntity;
    }
}
}