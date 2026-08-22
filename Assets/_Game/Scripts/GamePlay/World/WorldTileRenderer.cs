using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;

namespace _Game.Scripts.GamePlay.World
{
public class WorldTileRenderer: MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private GameObject _prefab;
    
    private readonly Dictionary<Biome, Tilemap> _tilemaps = new();
    private readonly Dictionary<Vector3Int, RenderedTile> _renderedTiles = new();
    
    public event Action<Vector3Int, Biome, Transform> OnTileCreated;
    public event Action<Vector3Int> OnTileUnloaded;
    public event Action<Vector3Int, Biome, Transform> OnTileLoaded;

    private WorldModel _model;

    [Inject]
    private void Construct(WorldModel model)
    {
        _model = model;
        CreateTileMaps();
    }
    
    private void CreateTileMaps()
    {
        foreach (var biome in _model.GetBiomes())
        {
            var biomeObject = Instantiate(_prefab, _grid);
            biomeObject.name = biome.Name;

            var tilemap = biomeObject.GetComponent<Tilemap>();
            _tilemaps.Add(biome, tilemap);
        }
    }

    public void TryPlaceTile(Vector3Int position)
    {
        if (_renderedTiles.TryGetValue(position, out var renderedTile))
        {
            LoadTile(position, renderedTile);
            return;
        }
        
        var biome = _model.GetBiome(position);
        CreateTile(position, biome);
    }
    
    private void LoadTile(Vector3Int position, RenderedTile renderedTile)
    {
        var biome = renderedTile.Biome;
        var tilemap = _tilemaps[biome];
        PlaceTile(tilemap, position, renderedTile.Tile);

        OnTileLoaded?.Invoke(position, biome, tilemap.transform);
    }

    private void CreateTile(Vector3Int position, Biome biome)
    {
        var tile = biome.Tile;
        var tilemap = _tilemaps[biome];
        PlaceTile(tilemap, position, tile);

        OnTileCreated?.Invoke(position, biome, tilemap.transform);

        _renderedTiles.Add(position, new RenderedTile(biome, tile));
    }

    public void TryUnloadTile(Vector3Int position)
    {
        if (!_renderedTiles.TryGetValue(position, out var renderedTile)) return;

        _tilemaps[renderedTile.Biome].SetTile(position, null);
        OnTileUnloaded?.Invoke(position);
    }

    private void PlaceTile(Tilemap tilemap, Vector3Int position, TileBase tile)
    {
        if (!tilemap.HasTile(position))
        {
            tilemap.SetTile(position, tile);
        }
    }
}
}