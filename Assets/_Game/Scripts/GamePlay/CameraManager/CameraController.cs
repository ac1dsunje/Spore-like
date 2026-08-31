using _Game.Scripts.GamePlay.Player;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.CameraManager
{
public class CameraController: MonoBehaviour
{
    [Inject] private Camera _camera;
    [Inject] private CinemachineCamera _cineMachineCamera;
    
    private PlayerRegistry _playerRegistry;

    public float Aspect => _camera.aspect;

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

    private void AddPlayer(PlayerController player)
    {
        _cineMachineCamera.Target.TrackingTarget = player.transform;
    }

    private void OnDestroy()
    {
        _playerRegistry.OnPlayerInitialized -= AddPlayer;
    }
}
}