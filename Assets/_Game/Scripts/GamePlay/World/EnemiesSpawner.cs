using System;
using System.Collections;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;
using CoroutineRunner = _Game.Scripts.Core.Services.CoroutineRunner;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.World
{
public class EnemiesSpawner : IStartable, IDisposable
{
    private readonly EntitiesRegistry _registry;
    private readonly EntitySpawner _spawner;
    private readonly WorldModel _world;
    private readonly CoroutineRunner _runner;

    private MovementModule _player;
        
    public EnemiesSpawner(EntitySpawner spawner, EntitiesRegistry registry, WorldModel world, CoroutineRunner runner)
    {
        _registry = registry;
        _world = world;
        _runner = runner;
        _spawner = spawner;
    }

    public void Start()
    {
        _registry.OnPlayerInitialized += AddPlayer;
    }

    private void AddPlayer(EntityScope entity)
    {
        _player = entity.Get<MovementModule>();
        _runner.Run(this, "Spawn Enemies", SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            var playerPos = _player.GridPosition.CurrentValue;
            var spawnPos = new Vector3Int(playerPos.x + Random.Range(-5, 5), playerPos.y + Random.Range(-5, 5), 0);
            var enemies = _world.GetBiome(spawnPos).Enemies;
            if (enemies.Count > 0)
                _spawner.SpawnEntity(new Vector2(spawnPos.x, spawnPos.y), enemies[Random.Range(0, enemies.Count)]);
        }
    }

    public void Dispose()
    {
        _runner.Stop(this);
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}