using _Game.Scripts.GamePlay.Entities;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.CameraManager
{
public class CameraController: MonoBehaviour
{
    [Inject] public Camera Camera { get; private set; }
    [Inject] private CinemachineCamera _cineMachineCamera;
    
    private PlayerRegistry _playerRegistry;

    public float Aspect => Camera.aspect;

    [Inject]
    private void Construct(PlayerRegistry playerRegistry)
    {
        _playerRegistry = playerRegistry;
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

    private void OnDestroy()
    {
        _playerRegistry.OnPlayerInitialized -= AddPlayer;
    }
}
}