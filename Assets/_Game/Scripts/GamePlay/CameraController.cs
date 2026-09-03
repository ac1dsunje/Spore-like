using System;
using _Game.Scripts.GamePlay.Entities;
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
    [Inject] private PlayerRegistry _playerRegistry;

    public float Aspect => Camera.aspect;

    public void Initialize()
    {
        _playerRegistry.OnPlayerInitialized += AddPlayer;
    }

    public void SetSize(float radius)
    {
        _cineMachineCamera.Lens.OrthographicSize = radius;
    }

    private void AddPlayer(EntityController player)
    {
        _cineMachineCamera.Target.TrackingTarget = player.Model.Movement.Transform;
    }

    public void Dispose()
    {
        _playerRegistry.OnPlayerInitialized -= AddPlayer;
    }
}
}