using UnityEngine;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private bool _spawnOnStart = true;

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