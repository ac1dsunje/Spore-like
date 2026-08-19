using _Game.Scripts.GamePlay.Entity;
using _Game.Scripts.GamePlay.Entity.Module;
using _Game.Scripts.GamePlay.Network;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public class PlayerBiome: EntityNetworkBehaviour
{
    private WorldModel _worldModel;
    private TemperatureModule _temperature;
    private MovementModule _movement;
    private BiomeModule _biome;
    private EntityStats _entityStats;
    
    private Biome _currentBiome;
    
    [Inject]
    private void Construct(WorldModel worldModel, TemperatureModule temperature, MovementModule movement,
        BiomeModule biome, EntityStats stats)
    {
        _worldModel = worldModel;
        _temperature = temperature;
        _movement = movement;
        _biome = biome;
        _entityStats = stats;
    }

    protected override void OnNetworkInitialized()
    {
        if (!IsLocal) return;
        _movement.OnGridPositionChanged += TryEnterBiome;
        EnterBiome(_worldModel.GetBiome(_movement.GridPosition));
    }

    private void TryEnterBiome(MovementModule player)
    {
        var currentBiome = _worldModel.GetBiome(player.GridPosition);
        if (currentBiome == _currentBiome) return;
        EnterBiome(currentBiome);
    }

    private void EnterBiome(Biome biome)
    {
        _entityStats.RemoveSource(_currentBiome);
        _currentBiome = biome;
        CheckPassability();
        ApplyTemperature(biome.Temperature);
    }

    private void CheckPassability()
    {
        if (_currentBiome.PassAbility > _biome.PassAbility)
        {
            _entityStats.AddSource(_currentBiome);
        }
    }

    private void ApplyTemperature(float temperature)
    {
        if (_temperature.IsLethal(temperature))
            Debug.Log($"Temperature {temperature} is lethal");
        else if (_temperature.IsUncomfortable(temperature))
            Debug.Log($"Temperature {temperature} is not comfortable");
        else
            Debug.Log($"Temperature {temperature} is comfortable");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}