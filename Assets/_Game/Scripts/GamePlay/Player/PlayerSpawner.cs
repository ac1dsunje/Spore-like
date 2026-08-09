using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private bool _spawnOnStart = true;
    [Inject] private IObjectResolver _objectResolver;

    private void Start()
    {
        if (_spawnOnStart)
        {
            Spawn();
        }
    }
    
    [ContextMenu("Spawn")]
    private void Spawn()
    {
        var spawnPoint = new Vector2(transform.position.x + Random.Range(-10, 11), transform.position.z + Random.Range(-10, 11));

        var player = _objectResolver.Instantiate(
            _playerPrefab,
            spawnPoint,
            Quaternion.identity);

        player.Initialize();
    }
}
}