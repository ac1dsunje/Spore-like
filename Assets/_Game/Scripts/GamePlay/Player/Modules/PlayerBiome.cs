using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public class PlayerBiome: MonoBehaviour
{
    private WorldModel _worldModel;
    
    private TemperatureModule _temperature;
    private BreathingModule _breathing;
    
    private MovementModule _movement;
    private BiomeModule _biome;
    private EntityStats _entityStats;
    
    private Biome _currentBiome;
    
    [Inject]
    private void Construct(WorldModel worldModel, TemperatureModule temperature, MovementModule movement,
        BiomeModule biome, EntityStats stats, BreathingModule breathing)
    {
        _worldModel = worldModel;
        _temperature = temperature;
        _breathing = breathing;
        _movement = movement;
        _biome = biome;
        _entityStats = stats;
        
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
        CheckBreathing(biome.OxygenBreathing, biome.HydrogenBreathing);
    }

    private void CheckPassability()
    {
        if (_currentBiome.PassAbility > _biome.PassAbility)
        {
            Debug.Log("Apply bad passability debuff");
        }
    }

    private void ApplyTemperature(float temperature)
    {
        if (_temperature.IsLethal(temperature))
            Debug.Log($"Temperature is lethal");
        else if (_temperature.IsUncomfortable(temperature))
            Debug.Log($"Temperature is not comfortable");
        else
            Debug.Log($"Temperature is comfortable");
    }

    private void CheckBreathing(float oxygen, float hydrogen)
    {
        var oxygenRequirement = _breathing.OxygenBreathing;
        var hydrogenRequirement = _breathing.HydrogenBreathing;
        
        if (oxygenRequirement> 0 && oxygenRequirement <= oxygen)
        {
            Debug.Log("Unset suffocating debuff");
        }
        else if (hydrogenRequirement > 0  && hydrogenRequirement <= hydrogen)
        {
            Debug.Log("Unset suffocating debuff");
        }
        else
        {
            Debug.Log("Apply suffocating debuff");
        }
    }

    private void OnDestroy()
    {
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}