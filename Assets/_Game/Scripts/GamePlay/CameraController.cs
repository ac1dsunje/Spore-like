using System;
using _Game.Scripts.GamePlay.Entities;
using Unity.Cinemachine;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class CameraController : IInitializable, IDisposable
{
    public Camera Camera { get; private set; }
    private readonly CinemachineCamera _cineMachineCamera;
    private readonly EntitiesRegistry _registry;

    public CameraController(Camera camera, CinemachineCamera cineMachineCamera, EntitiesRegistry registry)
    {
        Camera = camera;
        _cineMachineCamera = cineMachineCamera;
        _registry = registry;
    }

    public void Initialize()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    private void AddPlayer(EntityScope player)
    {
        _cineMachineCamera.Target.TrackingTarget = player.Get<Transform>();
        _cineMachineCamera.Lens.OrthographicSize = 6f;
    }

    public void Dispose()
    {
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}