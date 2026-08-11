using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World
{
public class WorldGenerator: MonoBehaviour
{
    [SerializeField] private int _renderDistance = 1;
    
    private readonly List<PlayerController> _players = new();
    
    private WorldModel _model;
    private PlayerRegistry _playerRegistry;
    private WorldTileRenderer _tileRendererGenerator;
    
    private readonly Dictionary<Vector3Int, int> _tileUsage = new();
    private readonly Dictionary<PlayerMovement, HashSet<Vector3Int>> _playerTiles = new();
    
    [Inject]
    private void Construct(WorldModel model, PlayerRegistry playerRegistry, WorldTileRenderer tileRendererGenerator)
    {
        _model = model;
        _playerRegistry = playerRegistry;
        _tileRendererGenerator = tileRendererGenerator;
        
        _playerRegistry.OnPlayerAdded += AddPlayer;
        _playerRegistry.OnPlayerRemoved += RemovePlayer;
    }

    private void AddPlayer(PlayerController player)
    {
        _players.Add(player);
        _playerTiles.Add(player.Movement, new HashSet<Vector3Int>());
        
        player.Movement.OnGridPositionChanged += Generate;
        Generate(player.Movement);
    }

    private void RemovePlayer(PlayerController player)
    {
        player.Movement.OnGridPositionChanged -= Generate;
        
        UnloadPlayerTiles(player.Movement);
        
        _playerTiles.Remove(player.Movement);
        _players.Remove(player);
    }

    private int GetDistance() => _renderDistance * _model.ChunkSize;
    
    private void Generate(PlayerMovement player)
    {
        var playerPosition = player.GridPosition;
        var distance = GetDistance();
        var currentTiles = _playerTiles[player];
        
        var newTiles = new HashSet<Vector3Int>();

        for (var x = playerPosition.x - distance; x <= playerPosition.x + distance; x++)
        {
            for (var y = playerPosition.y - distance; y <= playerPosition.y + distance; y++)
            {
                newTiles.Add(new Vector3Int(x, y, 0));
            }
        }

        foreach (var position in currentTiles.Where(position => !newTiles.Contains(position)))
        {
            RemoveTileUsage(position);
        }

        foreach (var position in newTiles.Where(position => !currentTiles.Contains(position)))
        {
            AddTileUsage(position);
        }

        currentTiles.Clear();

        foreach (var position in newTiles)
        {
            currentTiles.Add(position);
        }
    }

    private void AddTileUsage(Vector3Int position)
    {
        if (_tileUsage.TryGetValue(position, out var usage))
        {
            _tileUsage[position] = usage + 1;
            return;
        }

        _tileUsage.Add(position, 1);
        _tileRendererGenerator.TryPlaceTile(position);
    }

    private void RemoveTileUsage(Vector3Int position)
    {
        if (!_tileUsage.TryGetValue(position, out var usage))
            return;

        usage--;

        if (usage > 0)
        {
            _tileUsage[position] = usage;
            return;
        }

        _tileUsage.Remove(position);
        _tileRendererGenerator.TryUnloadTile(position);
    }

    private void UnloadPlayerTiles(PlayerMovement player)
    {
        foreach (var position in _playerTiles[player])
        {
            RemoveTileUsage(position);
        }
    }

    private void OnDestroy()
    {
        foreach (var player in _players)
        {
            player.Movement.OnGridPositionChanged -= Generate;
        }
        _playerRegistry.OnPlayerAdded -= AddPlayer;
        _playerRegistry.OnPlayerRemoved -= RemovePlayer;
    }
}
}