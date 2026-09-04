using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitiesRegistry : MonoBehaviour
{
    private EntitySpawner _spawner;
    public event Action<EntityController> OnPlayerInitialized;
    
    private readonly Dictionary<HealthModule, EntityScope> _entityByHealth = new();
    
    [Inject] 
    private void Construct(EntitySpawner spawner)
    {
        _spawner = spawner;
        _spawner.OnEntitySpawn += AddEntity;
        _spawner.OnPlayerSpawn += AddPlayer;
    }

    private void AddEntity(EntityScope entity)
    {
        var health = entity.GetEntityController().Model.Health;
        _entityByHealth.TryAdd(health, entity);
    }

    private void AddPlayer(EntityScope entity)
    {
        var player = entity.GetEntityController();
        var health = player.Model.Health;
        _entityByHealth.TryAdd(health, entity);
        OnPlayerInitialized?.Invoke(player);
    }

    public void DestroyEntityByHealth(HealthModule health)
    {
        if (!_entityByHealth.Remove(health, out var entity)) return;
        
        if (entity != null)
        {
            Destroy(entity.gameObject, 0.1f);
            entity.gameObject.SetActive(false);
        }
    }
    
    private void OnDestroy()
    {
        if (_spawner != null)
        {
            _spawner.OnEntitySpawn -= AddEntity;
            _spawner.OnPlayerSpawn -= AddPlayer;
        }
        _entityByHealth.Clear();
    }
}
}