using System;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private Vector2[] _spawnPoints;
    public event Action<PlayerController> OnPlayerSpawned;
    
    public PlayerController Spawn(Ticker ticker, int spawnIndex = 0)
    {
        var spawnPoint = _spawnPoints[spawnIndex];

        var player = Instantiate(
            _playerPrefab,
            spawnPoint,
            Quaternion.identity);

        player.Initialize(ticker);

        OnPlayerSpawned?.Invoke(player);
        return player;
    }
}
}