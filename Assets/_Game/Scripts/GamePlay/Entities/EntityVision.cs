using System;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityVision : IInitializable, IDisposable
{
    private readonly VisionModule _module;
    private readonly CameraController _camController;
    private readonly VisionHitbox _visionHitbox;

    public EntityVision(VisionModule module, CameraController camController, VisionHitbox visionHitbox)
    {
        _module = module;
        _camController = camController;
        _visionHitbox = visionHitbox;
    }

    public void Initialize()
    {
        _module.OnVisionRadiusUpdated += ApplyVision;

        _visionHitbox.OnEntityEntered += EnterEntity;
        _visionHitbox.OnEntityLeft += ExitEntity;
        
        ApplyVision(_module.VisionRadius);
    }

    private void EnterEntity(IVisible entity) => _module.EnterEntity(entity);

    private void ExitEntity(IVisible entity) => _module.ExitEntity(entity);

    private void ApplyVision(float value) => _visionHitbox.SetSize(new Vector2(_camController.Aspect, 1f) * (value * 2f));

    public void Dispose()
    {
        _module.OnVisionRadiusUpdated -= ApplyVision;

        _visionHitbox.OnEntityEntered -= EnterEntity;
        _visionHitbox.OnEntityLeft -= ExitEntity;
    }
}
}