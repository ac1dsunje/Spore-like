using System;
using System.Threading;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{
public class EnemiesSpawner : IStartable, IDisposable
{
    private readonly EntitiesRegistry _registry;
    private readonly EntitySpawner _spawner;
    private readonly WorldModel _world;
    private CancellationTokenSource _cts;

    private readonly float _spawnDelay = 3f;

    private MovementModule _player;
    
    public EnemiesSpawner(EntitySpawner spawner, EntitiesRegistry registry, WorldModel world)
    {
        _registry = registry;
        _world = world;
        _spawner = spawner;
    }

    public void Start()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    private void AddPlayer(EntityScope entity)
    {
        _player = entity.Get<MovementModule>();
        _cts = new CancellationTokenSource();
        SpawnEnemies().Forget();
    }

    private async UniTaskVoid SpawnEnemies()
    {
        const int min = 6;
        const int max = 10;
        const int minSqr = min * min;
        const int maxSqr = max * max;

        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay), cancellationToken: _cts.Token);
                var playerPos = _player.GridPosition.CurrentValue;

                int dx, dy;
                int distSqr;
                do
                {
                    dx = Random.Range(-max, max + 1);
                    dy = Random.Range(-max, max + 1);
                    distSqr = dx * dx + dy * dy;
                }
                while (distSqr < minSqr || distSqr > maxSqr);

                var spawnPos = new Vector3Int(playerPos.x + dx, playerPos.y + dy, 0);

                var enemies = _world.GetBiome(spawnPos).Enemies;
                if (enemies.Count > 0)
                    _spawner.SpawnEntity(new Vector2(spawnPos.x, spawnPos.y), enemies[Random.Range(0, enemies.Count)]);
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}