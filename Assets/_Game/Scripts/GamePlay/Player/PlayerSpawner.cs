using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [Inject] private IObjectResolver _objectResolver;
    [Inject] private PlayerRegistry _playerRegistry;
    
    [ContextMenu("Spawn")]
    private void Spawn()
    {
        var spawnPoint = new Vector2(transform.position.x + Random.Range(-10, 11), transform.position.z + Random.Range(-10, 11));

        var player = Instantiate(
            _playerPrefab,
            spawnPoint,
            Quaternion.identity,
            transform);

        player.Initialize();
    }
}
}