using System;
using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerVision : ITickable, IDisposable
{
    private VisionModule _module;
    private CameraController _camController;
    private VisionHitbox _visionHitbox;
    
    private float _targetVision;
    private float _currentVision;

    [Inject]
    private void Construct(VisionModule module, VisionHitbox visionHitbox, CameraController camController)
    {
        _module = module;
        _visionHitbox = visionHitbox;
        _camController = camController;
        
        _module.OnVisionRadiusUpdated += UpdateVision;
        _module.OnEntityDiscovered += ShowEntity;
        
        _currentVision = _module.VisionRadius;
        _targetVision = _module.VisionRadius;
        
        ApplyVision(_currentVision);
    }

    public void Tick()
    {
        if (Mathf.Approximately(_currentVision, _targetVision)) return;

        _currentVision = Mathf.MoveTowards(_currentVision, _targetVision, Time.deltaTime);

        ApplyVision(_currentVision);
    }

    private void ShowEntity(IVisible entity, bool state)
    {
        entity.SetVisible(state);
    }

    private void UpdateVision(float value) => _targetVision = value;

    private void ApplyVision(float value)
    {
        _visionHitbox?.SetSize(new Vector2(_camController.Aspect, 1f) * (value * 2f));

        _camController.SetSize(value);
    }

    public void Dispose()
    {
        _module.OnVisionRadiusUpdated -= UpdateVision;
        _module.OnEntityDiscovered -= ShowEntity;
    }
}
}