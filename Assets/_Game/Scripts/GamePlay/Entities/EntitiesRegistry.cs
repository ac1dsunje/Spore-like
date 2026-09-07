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
    private readonly HashSet<EntityScope> _allEntities = new();
    
    [Inject] 
    private void Construct(EntitySpawner spawner)
    {
        _spawner = spawner;
        _spawner.OnEntitySpawn += AddEntity;
        _spawner.OnPlayerSpawn += AddPlayer;
    }

    private void AddEntity(EntityScope entity)
    {
        var controller = entity.GetEntityController();
        var health = controller.Model.Health;
        
        _entityByHealth.TryAdd(health, entity);
        _allEntities.Add(entity);
    }

    private void AddPlayer(EntityScope entity)
    {
        var player = entity.GetEntityController();
        var health = player.Model.Health;
        
        _entityByHealth.TryAdd(health, entity);
        _allEntities.Add(entity);
        OnPlayerInitialized?.Invoke(player);
    }

    public void DestroyEntityByHealth(HealthModule health)
    {
        if (!_entityByHealth.Remove(health, out var entity)) return;
        _allEntities.Remove(entity);
        DestroyEntity(entity);
    }

    public void DestroyEntityByScope(EntityScope scope)
    {
        if (!_allEntities.Remove(scope)) return;
        
        var health = scope.GetEntityController().Model.Health;
        _entityByHealth.Remove(health);
        
        DestroyEntity(scope);
    }

    private void DestroyEntity(EntityScope entity)
    {
        if (entity == null) return;
        Destroy(entity.gameObject, 0.1f);
        entity.gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        if (_spawner != null)
        {
            _spawner.OnEntitySpawn -= AddEntity;
            _spawner.OnPlayerSpawn -= AddPlayer;
        }
        _entityByHealth.Clear();
        _allEntities.Clear();
    }
}
}