using System.Collections.Generic;
using _Game.Scripts.World.Biome;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.World
{
public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _chunkSize = 16;
    [SerializeField] private int _renderDistanceChunks = 1;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<BiomeConfig> _biomeConfigs;
    
    private Transform _player;

    public void Construct(Transform player)
    {
        _player = player;
        _tilemap.ClearAllTiles();
    }

    private Vector3Int _playerPos() => new(
                                            (int)_player.position.x, 
                                            (int)_player.position.y, 
                                            (int)_player.position.z
                                        );

    private void Update()
    {
        if (_player == null) return;
        Generate();
    }

    private int GetDistance() => _renderDistanceChunks * _chunkSize;

    private void Generate()
    {
        for (var x = _playerPos().x - GetDistance(); x < _playerPos().x + GetDistance(); x++)
        {
            for (var y = _playerPos().y - GetDistance(); y < _playerPos().y + GetDistance(); y++)
            {
                PlaceTile(new Vector3Int(x, y, 0));
            }
        }
    }

    private void PlaceTile(Vector3Int position)
    {
        if (_tilemap.HasTile(position)) return;
        
        _tilemap.SetTile(position, GetRandomBiomeConfig(_biomeConfigs).RandomTile);
    }

    private BiomeConfig GetRandomBiomeConfig(List<BiomeConfig> biomeConfigs)
    {
        return biomeConfigs[Random.Range(0, biomeConfigs.Count)];
    }
}
}