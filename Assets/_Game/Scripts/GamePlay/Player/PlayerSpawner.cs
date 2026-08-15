using UnityEngine;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private bool _spawnOnStart;

    private void Start()
    {
        if (_spawnOnStart) Spawn();
    }
    
    [ContextMenu("Spawn")]
    private void Spawn()
    {
        var player = Instantiate(
            _playerPrefab,
            transform);

        player.SetPlayer();
    }
}
}