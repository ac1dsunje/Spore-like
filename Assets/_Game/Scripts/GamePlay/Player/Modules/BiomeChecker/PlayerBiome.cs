using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.BiomeChecker
{
public class PlayerBiome: PlayerNetworkBehaviour
{
    private WorldModel _worldModel;
    private TemperatureModule _temperature;
    private MovementModule _movement;
    
    private Biome _currentBiome;
    
    [Inject]
    private void Construct(WorldModel worldModel, TemperatureModule temperature, MovementModule movement)
    {
        _worldModel = worldModel;
        _temperature = temperature;
        _movement = movement;
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
        _currentBiome = biome;
        ApplyTemperature(biome.Temperature);
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