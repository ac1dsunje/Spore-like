using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _playerPrefab;
    [SerializeField] private Vector2 _playerSpawnPoint;
    [SerializeField] private EntityConfig _playerConfig;

    [SerializeField] private EntityScope _enemyPrefab;
    [SerializeField] private Vector2 _enemySpawnPoint;
    [SerializeField] private EntityConfig _enemyConfig;
    
    private void Awake()
    {
        Spawn(_playerPrefab, _playerSpawnPoint, transform, _playerConfig);
        SpawnEnemy();
    }

    [ContextMenu("Spawn Enemy")]
    private void SpawnEnemy()
    {
        Spawn(_enemyPrefab, _enemySpawnPoint, transform, _enemyConfig);
    }
    
    public EntityScope Spawn(EntityScope entityScope, Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var entity = Instantiate(entityScope, spawnPoint, Quaternion.identity, parent);
        entity.SetConfig(entityConfig);
        entity.Build();
        return entity;
    }
}
}