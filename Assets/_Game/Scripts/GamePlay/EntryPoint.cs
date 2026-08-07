using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using Unity.Cinemachine;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Ticker _ticker;
    [SerializeField] private WorldGenerator _worldGenerator;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        _playerSpawner.OnPlayerSpawned += OnPlayerSpawned;
        _playerSpawner.Spawn(_ticker);
    }

    private void OnPlayerSpawned(PlayerController player)
    {
        _worldGenerator.Construct(player.transform);
        
        _camera.Target.TrackingTarget = player.transform;
        
        _uiManager.Construct(player.Model);
    }

    private void OnDestroy()
    {
        _playerSpawner.OnPlayerSpawned -= OnPlayerSpawned;
    }
}
}
