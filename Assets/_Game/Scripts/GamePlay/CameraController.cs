using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class CameraController : IInitializable, ITickable, IDisposable
{
    [Inject] public Camera Camera { get; private set; }
    [Inject] private CinemachineCamera _cineMachineCamera;
    [Inject] private EntitiesRegistry _registry;

    private VisionModule _playerVision;

    private float _currentSize;
    private float _targetSize;
    private bool _isInitialized;
    
    private readonly float _zoomSpeed = 5f; 

    public float Aspect => Camera.aspect;

    public void Initialize()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    public void Tick()
    {
        if (!_isInitialized) return;

        if (!Mathf.Approximately(_currentSize, _targetSize))
        {
            _currentSize = Mathf.MoveTowards(_currentSize, _targetSize, _zoomSpeed * Time.deltaTime);
            _cineMachineCamera.Lens.OrthographicSize = _currentSize;
        }
    }

    private void SetSize(float radius)
    {
        _targetSize = radius;
    }

    private void AddPlayer(EntityController player)
    {
        _playerVision = player.Model.Vision;
        _playerVision.OnVisionRadiusUpdated += SetSize;
        _cineMachineCamera.Target.TrackingTarget = player.Model.Movement.Transform;

        _targetSize = _playerVision.VisionRadius;
        _currentSize = _targetSize;
        _cineMachineCamera.Lens.OrthographicSize = _currentSize;
        _isInitialized = true;
    }

    public void Dispose()
    {
        if (_playerVision != null)
        {
            _playerVision.OnVisionRadiusUpdated -= SetSize;
        }
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}