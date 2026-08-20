using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class AdminUI: UIScreen
{
    [SerializeField] private Button _host;
    [SerializeField] private Button _connect;
    [SerializeField] private Button _player;
    
    [Inject] private NetworkManager _networkManager;
    [Inject] private PlayerSpawner _playerSpawner;

    private void Start()
    {
        _host.onClick.AddListener(() => _networkManager.StartHost());
        _connect.onClick.AddListener(() => _networkManager.StartClient());
        _player.onClick.AddListener(_playerSpawner.Spawn);
    }

    private void OnDestroy()
    {
        _host.onClick.RemoveAllListeners();
        _connect.onClick.RemoveAllListeners();
        _player.onClick.RemoveAllListeners();
    }
}
}