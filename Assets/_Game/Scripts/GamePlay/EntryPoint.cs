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
    
    [SerializeField] private WorldGenerationConfig _config;

    [SerializeField] private bool _spawnPlayerAtStart = true;
    
    private void Awake()
    {
        WorldModel model = new(_config);
        _worldGenerator.Construct(model, _playerSpawner);
        
        _playerSpawner.Construct(_ticker);
        
        _playerSpawner.OnPlayerSpawned += OnPlayerSpawned;

        if (_spawnPlayerAtStart)
        {
            _playerSpawner.Spawn();
        }
    }

    private void OnPlayerSpawned(PlayerController player)
    {
        _camera.Target.TrackingTarget = player.transform;
        
        _uiManager.Construct(player.Model);
    }

    private void OnDestroy()
    {
        _playerSpawner.OnPlayerSpawned -= OnPlayerSpawned;
    }
}
}
