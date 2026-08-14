using System.Collections.Generic;
using _Game.Scripts.GamePlay.World.Biomes;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{

public readonly struct SpawnedFood
{
    public readonly Transform Parent;
    public readonly FoodConfig Config;
        
    public  SpawnedFood(Transform parent, FoodConfig config)
    {
        Parent = parent;
        Config = config;
    }
}

public class EnvironmentSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
     
    private WorldTileRenderer _tileRenderer;
    
    private readonly Dictionary<Vector3Int, SpawnedFood> _spawnedFoods = new();
    private readonly Dictionary<Vector3Int, GameObject> _spawnedObjects = new();
    
    [Inject]
    private void Construct(WorldTileRenderer generator)
    {
        _tileRenderer = generator;
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
        if (!CanPlaceObject(biome.Config.ChanceEnvironment)) return;
        
        var environment = biome.Config.GetRandomEnvironment();
        if (!environment) return;
        
        var config = environment.FoodItems[Random.Range(0, environment.FoodItems.Length)];
        
        _spawnedFoods[position] = new(parent, config);
        SpawnPlant(position, parent, config);
    }

    private void UnloadEnvironment(Vector3Int position)
    {
        if (!_spawnedObjects.TryGetValue(position, out var item)) return;
        Destroy(item.gameObject, 1f);
        item.gameObject.SetActive(false);
        _spawnedObjects.Remove(position);
    }
    
    private bool CanPlaceObject(float chance) => Random.Range(0, 100) <= chance;

    private void SpawnPlant(Vector3Int setPos, Transform parent, FoodConfig config)
    {
        var position = new Vector3(setPos.x + 0.5f, setPos.y + 0.5f, setPos.z);
        var go = Instantiate(_foodPrefab, position, Quaternion.identity, parent);
        var item = go.GetComponent<FoodController>();
        item.SetConfig(config);
        _spawnedObjects[setPos] = go;
    }

    private void OnDestroy()
    {
        _tileRenderer.OnTileLoaded -= TryLoadEnvironment;
        _tileRenderer.OnTileCreated -= TryCreateEnvironment;
        _tileRenderer.OnTileUnloaded -= UnloadEnvironment;
    }
}
}