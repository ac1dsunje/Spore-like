using _Game.Scripts.GamePlay.Player;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.CameraManager
{
public class CameraController: MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineCamera _cineMachineCamera;
    
    private PlayerRegistry _playerRegistry;

    [Inject]
    private void Construct(PlayerRegistry playerRegistry)
    {
        _playerRegistry = playerRegistry;
        _playerRegistry.OnPlayerAdded += AddPlayer;
    }

    private void AddPlayer(PlayerController player)
    {
        _cineMachineCamera.Target.TrackingTarget = player.transform;
    }

    private void OnDestroy()
    {
        _playerRegistry.OnPlayerAdded -= AddPlayer;
    }
}
}