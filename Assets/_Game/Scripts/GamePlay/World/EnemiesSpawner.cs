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
        SpawnEnemies(_cts.Token).Forget();
    }

    private async UniTaskVoid SpawnEnemies(CancellationToken token)
    {
        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
                var playerPos = _player.GridPosition.CurrentValue;
                var spawnPos = new Vector3Int(playerPos.x + Random.Range(-5, 5), playerPos.y + Random.Range(-5, 5), 0);
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