using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{

public readonly struct SpawnedFood
{
    public readonly Transform Parent;
    public readonly EntityConfig Config;
        
    public  SpawnedFood(Transform parent, EntityConfig config)
    {
        Parent = parent;
        Config = config;
    }
}

public class EnvironmentSpawner: IStartable, IDisposable
{
    private WorldTileRenderer _tileRenderer;
    private EntitySpawner _spawner;
    private EntitiesRegistry _entitiesRegistry;
    
    private readonly Dictionary<Vector3Int, SpawnedFood> _spawnedFoods = new();
    private readonly Dictionary<Vector3Int, EntityScope> _spawnedObjects = new();
    
    [Inject]
    private void Construct(WorldTileRenderer generator, EntitySpawner spawner, EntitiesRegistry entitiesRegistry)
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
    
    private void TryLoadEnvironment(Vector3Int position, Biome biome, Transform parent)
    {
        if (!_spawnedFoods.TryGetValue(position, out var item)) return;
        SpawnPlant(position, item.Parent, item.Config);
    }

    private void TryCreateEnvironment(Vector3Int position, Biome biome, Transform parent)
    {
        if (!CanPlaceObject(biome.ChanceEnvironment)) return;
        
        var environment = biome.GetRandomEnvironment();
        if (!environment) return;
        
        var config = environment.FoodItems[Random.Range(0, environment.FoodItems.Length)];
        
        _spawnedFoods[position] = new(parent, config);
        SpawnPlant(position, parent, config);
    }

    private void UnloadEnvironment(Vector3Int position)
    {
        if (!_spawnedObjects.TryGetValue(position, out var item)) return;
        
        if (!item)
        {
            _spawnedObjects.Remove(position);
            _spawnedFoods.Remove(position);
            return;
        }
        _entitiesRegistry.DestroyEntityByScope(item);
        _spawnedObjects.Remove(position);
    }
    
    private bool CanPlaceObject(float chance) => Random.Range(0, 100) <= chance;

    private void SpawnPlant(Vector3Int setPos, Transform parent, EntityConfig config)
    {
        var position = new Vector3(setPos.x + 0.5f, setPos.y + 0.5f, setPos.z);
        
        _spawnedObjects[setPos] = _spawner.SpawnEntity(position, parent, config);
    }

    public void Dispose()
    {
        _tileRenderer.OnTileLoaded -= TryLoadEnvironment;
        _tileRenderer.OnTileCreated -= TryCreateEnvironment;
        _tileRenderer.OnTileUnloaded -= UnloadEnvironment;
    }
}
}