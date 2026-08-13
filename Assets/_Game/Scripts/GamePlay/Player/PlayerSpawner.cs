using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private bool _spawnOnStart = true;
    [Inject] private IObjectResolver _objectResolver;
    [Inject] private PlayerRegistry _playerRegistry;

    private void Start()
    {
        if (_spawnOnStart) Spawn();
    }
    
    private void Spawn()
    {
        var player = Instantiate(
            _playerPrefab,
            transform);

        player.SetSinglePlayer();
    }
}
}