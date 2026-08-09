using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private WorldGenerationConfig _config;
    [SerializeField] private bool _spawnPlayerAtStart = true;
    
    [Inject] private WorldGenerator _worldGenerator;
    [Inject] private PlayerSpawner _playerSpawner;
    [Inject] private UIManager _uiManager;
    
    private void Awake()
    {
        WorldModel model = new(_config);
        _worldGenerator.Construct(model);
    
        _playerSpawner.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void Start()
    {
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
