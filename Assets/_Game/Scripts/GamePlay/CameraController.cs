using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class CameraController: IInitializable, IDisposable
{
    [Inject] public Camera Camera { get; private set; }
    [Inject] private CinemachineCamera _cineMachineCamera;
    [Inject] private EntitiesRegistry _registry;

    private VisionModule _playerVision;

    public float Aspect => Camera.aspect;

    public void Initialize()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    private void SetSize(float radius)
    {
        _cineMachineCamera.Lens.OrthographicSize = radius;
    }

    private void AddPlayer(EntityController player)
    {
        _playerVision = player.Model.Vision;
        _playerVision.OnVisionRadiusUpdated += SetSize;
        _cineMachineCamera.Target.TrackingTarget = player.Model.Movement.Transform;
    }

    public void Dispose()
    {
        _playerVision.OnVisionRadiusUpdated -= SetSize;
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}