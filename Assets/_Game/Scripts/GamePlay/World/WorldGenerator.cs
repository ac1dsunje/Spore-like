using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World
{
public class WorldGenerator: IInitializable, IDisposable
{
    private const int RenderDistance = 1;

    private readonly WorldModel _model;
    private readonly EntitiesRegistry _registry;
    
    private MovementModule _player;
    private readonly HashSet<Vector3Int> _loadedTiles = new();
    
    public event Action<Vector3Int> OnTileAddRequested;
    public event Action<Vector3Int> OnTileRemoveRequested;
    
    public WorldGenerator(WorldModel model, EntitiesRegistry registry)
    {
        _model = model;
        _registry = registry;
    }

    public void Initialize()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    private void AddPlayer(EntityController player)
    {
        _player = player.Model.Movement;
        _player.OnGridPositionChanged += Generate;
        Generate(_player.GridPosition);
    }

    private int GetDistance() => RenderDistance * _model.ChunkSize;
    
    private void Generate(Vector3Int pos)
    {
        var distance = GetDistance();
        
        var newTiles = new HashSet<Vector3Int>();

        for (var x = pos.x - distance; x <= pos.x + distance; x++)
        {
            for (var y = pos.y - distance; y <= pos.y + distance; y++)
            {
                newTiles.Add(new Vector3Int(x, y, 0));
            }
        }

        foreach (var position in _loadedTiles)
        {
            if (!newTiles.Contains(position))
            {
                OnTileRemoveRequested?.Invoke(position);
            }
        }

        foreach (var position in newTiles)
        {
            if (!_loadedTiles.Contains(position))
            {
                OnTileAddRequested?.Invoke(position);
            }
        }

        _loadedTiles.Clear();
        foreach (var position in newTiles)
        {
            _loadedTiles.Add(position);
        }
    }

    public void Dispose()
    {
        if (_player != null)
        {
            _player.OnGridPositionChanged -= Generate;
        }
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}