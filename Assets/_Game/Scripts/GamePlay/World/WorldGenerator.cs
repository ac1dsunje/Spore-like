using System.Collections.Generic;
using _Game.Scripts.GamePlay.World.Biome;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{

public readonly struct RenderedTile
{
    public readonly int BiomeIndex;
    public readonly TileBase Tile;

    public RenderedTile(int biomeIndex, TileBase tile)
    {
        BiomeIndex = biomeIndex;
        Tile = tile;
    }
}

public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _renderDistance = 1;
    [SerializeField] private Transform _grid;
    [SerializeField] private Tilemap _prefab;
    private readonly List<Tilemap> _tilemaps = new();
    
    private Transform _player;
    private WorldModel _model;

    private Vector3Int _lastPlayerPosition;
    
    private readonly Dictionary<Vector3Int, RenderedTile> _renderedTiles = new();

    public void Construct(Transform player, WorldModel model)
    {
        _model = model;
        _player = player;
        for (var i = 0; i < _model.BiomeCount; i++)
        {
            var map = Instantiate(_prefab, _grid);
            _tilemaps.Add(map);
        }
        Generate();
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

        if (!HasPlayerMoved()) return;
        Generate();
    }

    private bool HasPlayerMoved()
    {
        if (_playerPos() == _lastPlayerPosition) return false;
        _lastPlayerPosition = _playerPos();
        return true;
    }

    private int GetDistance() => _renderDistance * _model.ChunkSize;

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
}
}