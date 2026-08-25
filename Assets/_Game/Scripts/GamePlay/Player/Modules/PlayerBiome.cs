using _Game.Scripts.GamePlay.Buffs;
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
    private BiomeModule _biomeModule;
    
    private BuffsModule _buffsModule;
    
    private Biome _currentBiome;
    
    [Inject]
    private void Construct(WorldModel worldModel, TemperatureModule temperature, MovementModule movement,
        BiomeModule biome, BreathingModule breathing, BuffsModule buffsModule)
    {
        _worldModel = worldModel;
        _temperature = temperature;
        _breathing = breathing;
        _movement = movement;
        _biomeModule = biome;
        _buffsModule = buffsModule;
        
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
        _buffsModule.Set(BuffType.BadPassAbility, _currentBiome.PassAbility > _biomeModule.PassAbility);
    }

    private void ApplyTemperature(float temperature)
    {
        _buffsModule.Set(BuffType.Cold, temperature < _temperature.MinimalComfortable);
        _buffsModule.Set(BuffType.Heat, temperature > _temperature.MaximumComfortable);
    }

    private void CheckBreathing(float oxygen, float hydrogen)
    {
        var oxygenRequirement = _breathing.OxygenBreathing;
        var hydrogenRequirement = _breathing.HydrogenBreathing;
        
        if (oxygenRequirement> 0 && oxygenRequirement <= oxygen)
        {
            _buffsModule.Set(BuffType.Suffocating, false);
        }
        else if (hydrogenRequirement > 0  && hydrogenRequirement <= hydrogen)
        {
            _buffsModule.Set(BuffType.Suffocating, false);
        }
        else
        {
            _buffsModule.Set(BuffType.Suffocating, true);
        }
    }

    private void OnDestroy()
    {
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}