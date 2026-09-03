using System;
using _Game.Scripts.GamePlay.Entities.Configuration;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _entityPrefab;
    [SerializeField] private EntityConfig _playerConfig;

    [SerializeField] private Vector2 _enemySpawnPoint;
    [SerializeField] private EntityConfig _enemyConfig;
    
    [Inject] private PlayerRegistry _playerRegistry;

    public event Action<EntityScope> OnEntitySpawn;
    
    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var player = Spawn(transform.position, transform, _playerConfig);
        _playerRegistry.AddPlayer(player.GetEntityController());
    }

    [ContextMenu("Spawn Enemy")]
    private void SpawnEnemy()
    {
        var enemy = Spawn(_enemySpawnPoint, transform, _enemyConfig);
        OnEntitySpawn?.Invoke(enemy);
    }

    public EntityScope SpawnPlant(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var plant = Spawn(spawnPoint, parent, entityConfig);
        OnEntitySpawn?.Invoke(plant);
        return plant;
    }
    
    private EntityScope Spawn(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var entity = Instantiate(_entityPrefab, spawnPoint, Quaternion.identity, parent);
        entity.SetConfig(entityConfig);
        entity.Build();
        return entity;
    }
}
}