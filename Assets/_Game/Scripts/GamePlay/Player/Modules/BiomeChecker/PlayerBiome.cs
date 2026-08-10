using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.BiomeChecker
{
public class PlayerBiome: MonoBehaviour
{
    private Biome _currentBiome;
    private PlayerMovement _movement;
    private WorldModel _worldModel;
    private PlayerModel _model;
    
    public void Construct(PlayerMovement movement, WorldModel worldModel, PlayerModel model)
    {
        _movement = movement;
        _worldModel = worldModel;
        _model = model;
        
        _movement.OnGridPositionChanged += TryEnterBiome;
        EnterBiome(worldModel.GetBiome(_movement.GridPosition));
    }

    private void TryEnterBiome(PlayerMovement player)
    {
        var currentBiome = _worldModel.GetBiome(player.GridPosition);
        if (currentBiome == _currentBiome) return;
        EnterBiome(currentBiome);
    }

    private void EnterBiome(Biome biome)
    {
        _currentBiome = biome;
        Debug.Log("Entering biome: " + biome.Name);

        ApplyTemperature(biome.Temperature);
    }

    private void ApplyTemperature(float temperature)
    {
        if (_model.Temperature.IsLethal(temperature))
        {
            Debug.Log($"Temperature {temperature} is lethal");
        }
        else if (_model.Temperature.IsUncomfortable(temperature))
        {
            Debug.Log($"Temperature {temperature} is not comfortable");
        }
        else
        {
            Debug.Log($"Temperature {temperature} is comfortable");
        }
    }

    private void OnDestroy()
    {
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}