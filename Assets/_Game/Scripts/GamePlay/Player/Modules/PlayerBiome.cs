using _Game.Scripts.GamePlay.Module;
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
    
    private Biome _currentBiome;
    
    [Inject]
    private void Construct(WorldModel worldModel, TemperatureModule temperature, MovementModule movement, BiomeModule biome)
    {
        _worldModel = worldModel;
        _temperature = temperature;
        _movement = movement;
        _biome = biome;
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
        CheckPassability();
        ApplyTemperature(biome.Temperature);
    }

    private void CheckPassability()
    {
        if (_currentBiome.PassAbility > _biome.PassAbility)
        {
            Debug.Log("Get affected by biome");
        }
        else
        {
            Debug.Log("You can pass this biome");
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