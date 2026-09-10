using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{

public class EnvironmentSpawner: IStartable, IDisposable
{
    private readonly WorldTileRenderer _tileRenderer;
    private readonly EntitySpawner _spawner;
    private readonly EntitiesRegistry _entitiesRegistry;
    
    private readonly Dictionary<Vector3Int, EntityConfig> _spawnedEntities = new();
    private readonly Dictionary<Vector3Int, EntityScope> _spawnedObjects = new();
    
    
    public EnvironmentSpawner(WorldTileRenderer generator, EntitySpawner spawner, EntitiesRegistry entitiesRegistry)
    {
        _tileRenderer = generator;
        _spawner = spawner;
        _entitiesRegistry = entitiesRegistry;
    }

    public void Start()
    {
        _tileRenderer.OnTileCreated += TryCreateEnvironment;
        _tileRenderer.OnTileLoaded += TryLoadEnvironment;
        _tileRenderer.OnTileUnloaded += UnloadEnvironment;
    }
    
    private void TryLoadEnvironment(Vector3Int position, Biome biome)
    {
        if (!_spawnedEntities.TryGetValue(position, out var item)) return;
        SpawnPlant(position, item);
    }

    private void TryCreateEnvironment(Vector3Int position, Biome biome)
    {
        if (!CanPlaceObject(biome.ChanceEnvironment)) return;
        
        var environment = biome.GetRandomEnvironment();
        if (environment == null) return;
        
        var config = environment.Entity;
        
        _spawnedEntities[position] = config;
        SpawnPlant(position, config);
    }

    private void UnloadEnvironment(Vector3Int position)
    {
        if (!_spawnedObjects.TryGetValue(position, out var item)) return;
        
        if (!item)
        {
            _spawnedObjects.Remove(position);
            _spawnedEntities.Remove(position);
            return;
        }
        _entitiesRegistry.DestroyEntityByScope(item);
        _spawnedObjects.Remove(position);
    }
    
    private bool CanPlaceObject(float chance) => Random.Range(0, 100) <= chance;

    private void SpawnPlant(Vector3Int setPos, EntityConfig config)
    {
        var position = new Vector3(setPos.x + 0.5f, setPos.y + 0.5f, setPos.z);
        
        _spawnedObjects[setPos] = _spawner.SpawnEntity(position, config);
    }

    public void Dispose()
    {
        _tileRenderer.OnTileLoaded -= TryLoadEnvironment;
        _tileRenderer.OnTileCreated -= TryCreateEnvironment;
        _tileRenderer.OnTileUnloaded -= UnloadEnvironment;
    }
}
}