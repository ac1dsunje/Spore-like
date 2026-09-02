using System;
using UnityEngine;
using VContainer;

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
    
    [Inject] private PlayerRegistry _playerRegistry;

    public event Action<EntityScope> OnEntitySpawn;
    
    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var player = Spawn(_playerPrefab, transform.position, transform, _playerConfig);
        _playerRegistry.AddPlayer(player.GetEntityController());
    }

    [ContextMenu("Spawn Enemy")]
    private void SpawnEnemy()
    {
        var enemy = Spawn(_enemyPrefab, _enemySpawnPoint, transform, _enemyConfig);
        OnEntitySpawn?.Invoke(enemy);
    }

    public EntityScope SpawnPlant(Vector2 spawnPoint, Transform parent, EntityConfig entityConfig)
    {
        var plant = Spawn(_plantPrefab, spawnPoint, parent, entityConfig);
        OnEntitySpawn?.Invoke(plant);
        return plant;
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