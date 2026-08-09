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

    public void Construct(WorldModel model)
    {
        _model = model;
        for (var i = 0; i < _model.BiomeCount; i++)
        {
            var map = Instantiate(_prefab, _grid);
            _tilemaps.Add(map);
        }
    }

    public void AddPlayer(PlayerMovement player)
    {
        _player = player;
        _player.OnGridPositionChanged += Generate;
        Generate();
    }

    private int GetDistance() => _renderDistance * _model.ChunkSize;
    
    private bool IsInRenderDistance(Vector3Int position, Vector3Int center, int distance)
    {
        return Mathf.Abs(position.x - center.x) <= distance && Mathf.Abs(position.y - center.y) <= distance;
    }

    private void Generate()
    {
        var playerPosition = _player.GetGridPosition();
        var distance = GetDistance();
        var unloadDistance = distance + _model.ChunkSize;
        
        for (var x = playerPosition.x - unloadDistance; x <= playerPosition.x + unloadDistance; x++)
        {
            for (var y = playerPosition.y - unloadDistance; y <= playerPosition.y + unloadDistance; y++)
            {
                var position = new Vector3Int(x, y, 0);

                if (IsInRenderDistance(position, playerPosition, distance))
                    TryPlaceTile(position);
                else
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