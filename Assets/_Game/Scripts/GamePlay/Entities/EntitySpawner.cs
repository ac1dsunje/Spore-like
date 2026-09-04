using System;
using _Game.Scripts.GamePlay.Entities.Configuration;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _entityPrefab;
    [SerializeField] private EntityConfig _playerConfig;

    [SerializeField] private Vector2 _enemySpawnPoint;
    [SerializeField] private EntityConfig _enemyConfig;

    public event Action<EntityScope> OnEntitySpawn;
    public event Action<EntityScope> OnPlayerSpawn;
    
    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var player = Spawn(transform.position, transform, _playerConfig);
        OnPlayerSpawn?.Invoke(player);
    }

    [ContextMenu("Spawn Enemy")]
    private void SpawnEnemy()
    {
        var enemy = Spawn(_enemySpawnPoint, transform, _enemyConfig);
        OnEntitySpawn?.Invoke(enemy);
    }

    public EntityScope SpawnEntity(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var entity = Spawn(spawnPoint, parent, entityConfig);
        OnEntitySpawn?.Invoke(entity);
        return entity;
    }
    
    private EntityScope Spawn(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var entity = Instantiate(_entityPrefab, spawnPoint, Quaternion.identity, parent);
        entity.gameObject.name = entityConfig.name;
        entity.SetConfig(entityConfig);
        entity.Build();
        return entity;
    }
}
}