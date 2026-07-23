using System.Collections.Generic;
using _Game.Scripts.Rarities;
using _Game.Scripts.World.Chunk.Environment.Plants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.World.Chunk
{
public class Chunk : MonoBehaviour
{
    [SerializeField] private ChunkConfig _config;
    [SerializeField] private Transform[] _positions;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;

    private readonly List<Transform> _busyPositions = new();
    
    private void Awake()
    {
        SpawnEnvironment();
    }

    private void SpawnEnvironment()
    {
        SpawnPlants();
        SpawnSpikes();
    }

    private void SpawnPlants()
    {
        foreach (var spawnPoint in _positions)
        {
            if (!IsAbleToSpawn(_config.Environment.Plants.Chance, spawnPoint)) continue;

            SetRandomPlant(spawnPoint.position);
            
            _busyPositions.Add(spawnPoint); 
        }
    }
    
    private void SpawnSpikes()
    {
        foreach (var spawnPoint in _positions)
        {
            if (!IsAbleToSpawn(_config.Environment.Spikes.Chance, spawnPoint)) continue;

            SetRandomSpike(spawnPoint.position);
            
            _busyPositions.Add(spawnPoint); 
        }
    }

    private bool IsAbleToSpawn(float chance, Transform pos)
    {
        var rand = Random.Range(0, 100);
        return (rand < chance) & !_busyPositions.Contains(pos);
    }

    private void SetRandomPlant(Vector2 pos)
    {
        var rand = Random.Range(0, _config.Environment.Plants.Prefabs.Length);
            
        var obj = Instantiate(_config.Environment.Plants.Prefabs[rand], pos, Quaternion.identity, transform).GetComponent<Plant>();
        obj.SetRarity(_raritiesDatabase);
    }
    
    private void SetRandomSpike(Vector2 pos)
    {
        var rand = Random.Range(0, _config.Environment.Spikes.Prefabs.Length);
            
        Instantiate(_config.Environment.Spikes.Prefabs[rand], pos, Quaternion.identity, transform);
    }
}
}