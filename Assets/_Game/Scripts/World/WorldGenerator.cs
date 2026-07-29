using System.Collections.Generic;
using _Game.Scripts.World.Biome;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Game.Scripts.World
{
public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _chunkSize = 16;
    [SerializeField] private int _renderDistance = 1;
    [SerializeField] private List<BiomeConfig> _biomeConfigs;
    [SerializeField] private Tilemap[] _tilemaps;
    
    [Header("Noise Settings")]
    [SerializeField] private float _scale = 0.03f;
    [SerializeField] private int _seed;
    
    private Transform _player;

    private void Awake()
    {
        _seed = Random.Range(0, 99999);
    }

    public void Construct(Transform player)
    {
        _player = player;
        foreach (var tilemap in _tilemaps)
        {
            tilemap.ClearAllTiles();
        }
    }

    private Vector3Int _playerPos() => 
        new(
            (int)_player.position.x, 
            (int)_player.position.y, 
            (int)_player.position.z
        );

    private void Update()
    {
        if (_player == null) return;
        Generate();
    }

    private int GetDistance() => _renderDistance * _chunkSize;

    private void Generate()
    {
        for (var x = _playerPos().x - GetDistance(); x < _playerPos().x + GetDistance(); x++)
        {
            for (var y = _playerPos().y - GetDistance(); y < _playerPos().y + GetDistance(); y++)
            {
                TryPlaceTile(new Vector3Int(x, y, 0));
            }
        }
    }

    private void TryPlaceTile(Vector3Int position)
    {
        var biome = CheckBiome(position);
        if (_tilemaps[biome.Height].HasTile(position)) return;
        
        _tilemaps[biome.Height].SetTile(position,CheckBiome(position).RandomTile);
        TryPlaceEnvironment(position, biome);
    }

    private void TryPlaceEnvironment(Vector3Int position, BiomeConfig biome)
    {
        var rand = Random.Range(0, 100);
        if (rand >= biome.ChanceEnvironment) return;
        var environment = biome.GetRandomEnvironment();
        Instantiate(environment.Prefabs[Random.Range(0, environment.Prefabs.Length)], new Vector3(position.x + 0.5f, position.y + 0.5f, position.z), Quaternion.identity, _tilemaps[biome.Height].transform);
    }

    private BiomeConfig CheckBiome(Vector3Int position)
    {
        var x = (position.x + _seed * 1000f) * _scale;
        var y = (position.y + _seed * 1000f) * _scale;
    
        var noiseValue = Mathf.PerlinNoise(x, y);
    
        var biomeCount = _biomeConfigs.Count;
        var biomeIndex = Mathf.FloorToInt(noiseValue * biomeCount);
    
        biomeIndex = Mathf.Clamp(biomeIndex, 0, biomeCount - 1);
    
        return _biomeConfigs[biomeIndex];
    }
}
}