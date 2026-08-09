using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private bool _spawnPlayerAtStart = true;
    
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private UIManager _uiManager;
    
    private void Awake()
    {
        _playerRegistry.OnPlayerAdded += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(PlayerController player)
    {
        _camera.Target.TrackingTarget = player.transform;
        
        _uiManager.Construct(player.Model);
    }

    private void OnDestroy()
    {
        _playerRegistry.OnPlayerAdded -= OnPlayerSpawned;
    }
}
}
