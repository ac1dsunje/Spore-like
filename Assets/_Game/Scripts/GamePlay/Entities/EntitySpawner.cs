using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _playerPrefab;
    [SerializeField] private EntityConfig _playerConfig;

    [SerializeField] private EntityScope _enemyPrefab;
    [SerializeField] private Vector2 _enemySpawnPoint;
    [SerializeField] private EntityConfig _enemyConfig;
    
    [SerializeField] private EntityScope _plantPrefab;
    
    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer() => Spawn(_playerPrefab, transform.position, transform, _playerConfig);

    [ContextMenu("Spawn Enemy")]
    private void SpawnEnemy() => Spawn(_enemyPrefab, _enemySpawnPoint, transform, _enemyConfig);

    public EntityScope SpawnPlant(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        return Spawn(_plantPrefab, spawnPoint, parent, entityConfig);
    }
    
    private EntityScope Spawn(EntityScope entityScope, Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var entity = Instantiate(entityScope, spawnPoint, Quaternion.identity, parent);
        entity.SetConfig(entityConfig);
        entity.Build();
        return entity;
    }
}
}