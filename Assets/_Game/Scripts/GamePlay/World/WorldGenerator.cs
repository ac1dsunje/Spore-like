using System.Collections.Generic;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World
{
public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _renderDistance = 1;
    
    private WorldModel _model;
    private PlayerRegistry _playerRegistry;
    private WorldTileRenderer _tileRendererGenerator;
    
    private MovementModule _player;
    private readonly HashSet<Vector3Int> _loadedTiles = new();
    
    [Inject]
    private void Construct(WorldModel model, PlayerRegistry playerRegistry, WorldTileRenderer tileRendererGenerator)
    {
        _model = model;
        _playerRegistry = playerRegistry;
        _tileRendererGenerator = tileRendererGenerator;
        
        _playerRegistry.OnPlayerInitialized += InitializePlayer;
    }

    private void InitializePlayer(PlayerController player)
    {
        _player = player.Model.Movement;
        _player.OnGridPositionChanged += Generate;
        Generate(_player.GridPosition);
    }

    private int GetDistance() => _renderDistance * _model.ChunkSize;
    
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
                _tileRendererGenerator.TryUnloadTile(position);
            }
        }

        foreach (var position in newTiles)
        {
            if (!_loadedTiles.Contains(position))
            {
                _tileRendererGenerator.TryPlaceTile(position);
            }
        }

        _loadedTiles.Clear();
        foreach (var position in newTiles)
        {
            _loadedTiles.Add(position);
        }
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnGridPositionChanged -= Generate;
        }
        _playerRegistry.OnPlayerInitialized -= InitializePlayer;
    }
}
}