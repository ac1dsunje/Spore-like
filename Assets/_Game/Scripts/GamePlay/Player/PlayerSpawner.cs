using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    public event Action<PlayerController> OnPlayerSpawned;
    public event Action<PlayerController> OnPlayerRemoved;

    private Ticker _ticker;

    public void Construct(Ticker ticker)
    {
        _ticker = ticker;
    }
    
    [ContextMenu("Spawn")]
    public void Spawn()
    {
        var spawnPoint = new Vector2(transform.position.x + Random.Range(-10, 11), transform.position.z + Random.Range(-10, 11));

        var player = Instantiate(
            _playerPrefab,
            spawnPoint,
            Quaternion.identity);

        player.Initialize(_ticker);

        OnPlayerSpawned?.Invoke(player);
    }
}
}