using System.Collections.Generic;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.World.Biome;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{
public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _renderDistance = 1;
    [SerializeField] private Transform _grid;
    [SerializeField] private Tilemap _prefab;
    private readonly List<Tilemap> _tilemaps = new();
    
    private PlayerMovement _player;
    private WorldModel _model;
    
    private readonly Dictionary<Vector3Int, RenderedTile> _renderedTiles = new();

    public void Construct(PlayerMovement player, WorldModel model)
    {
        _model = model;
        _player = player;
        _player.OnGridPositionChanged += Generate;
        for (var i = 0; i < _model.BiomeCount; i++)
        {
            var map = Instantiate(_prefab, _grid);
            _tilemaps.Add(map);
        }
        Generate();
    }

    private int GetDistance() => _renderDistance * _model.ChunkSize;

    private void Generate()
    {
        var playerPosition = _player.GetGridPosition();
        var distance = GetDistance();
        var unloadDistance = distance + _model.ChunkSize;
        
        LoadTiles(playerPosition, distance);

        UnloadTiles(playerPosition, distance, unloadDistance);
    }

    private void LoadTiles(Vector3Int playerPosition, int distance)
    {
        for (var x = playerPosition.x - distance; x <= playerPosition.x + distance; x++)
        {
            for (var y = playerPosition.y - distance; y <= playerPosition.y + distance; y++)
            {
                TryPlaceTile(new Vector3Int(x, y, 0));
            }
        }
    }

    private void UnloadTiles(Vector3Int playerPosition, int distance, int unloadDistance)
    {
        for (var x = playerPosition.x - unloadDistance; x <= playerPosition.x + unloadDistance; x++)
        {
            for (var y = playerPosition.y - unloadDistance; y <= playerPosition.y + unloadDistance; y++)
            {
                var position = new Vector3Int(x, y, 0);

                if (Mathf.Abs(x - playerPosition.x) <= distance && Mathf.Abs(y - playerPosition.y) <= distance)
                {
                    continue;
                }

                TryUnloadTile(position);
            }
        }
    }

    private void TryUnloadTile(Vector3Int position)
    {
        if (!_renderedTiles.TryGetValue(position, out var renderedTile))
            return;

        _tilemaps[renderedTile.BiomeIndex].SetTile(position, null);
    }

    private void TryPlaceTile(Vector3Int position)
    {
        if (_renderedTiles.TryGetValue(position, out var renderedTile))
        {
            var tilemap = _tilemaps[renderedTile.BiomeIndex];

            if (!tilemap.HasTile(position))
            {
                tilemap.SetTile(position, renderedTile.Tile);
            }

            return;
        }

        var biome = _model.GetBiome(position);
        var tile = biome.RandomTile;

        _tilemaps[biome.Index].SetTile(position, tile);

        _renderedTiles.Add(position, new RenderedTile(biome.Index, tile));

        TryPlaceEnvironment(position, biome);
    }

    private void TryPlaceEnvironment(Vector3Int position, BiomeConfig biome)
    {
        var rand = Random.Range(0, 100);
        if (rand >= biome.ChanceEnvironment) return;
        var environment = biome.GetRandomEnvironment();
        var prefab = environment.Prefabs[Random.Range(0, environment.Prefabs.Length)];
        var setPos = new Vector3(position.x + 0.5f, position.y + 0.5f, position.z);
        Instantiate(prefab, setPos, Quaternion.identity, _tilemaps[biome.Index].transform);
    }

    private void OnDestroy()
    {
        _player.OnGridPositionChanged -= Generate;
    }
}
}