using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World.Biome
{
[CreateAssetMenu(fileName = "New Biome Config", menuName = "Configs/Game/World/Biomes/Biome")]
public class BiomeConfig: ScriptableObject
{
    [field: SerializeField] public List<TileBase> Tiles { get; private set; }
    [field: SerializeField] public List<EnvironmentConfig> EnvironmentConfigs { get; private set; }
    [field: SerializeField] public int ChanceEnvironment { get; private set; } = 20;

    public TileBase RandomTile => Tiles[Random.Range(0, Tiles.Count)];

    public EnvironmentConfig GetRandomEnvironment()
    {
        if (EnvironmentConfigs.Count == 0) return null;
        
        var totalChance = EnvironmentConfigs.Sum(config => config.Chance);

        if (totalChance <= 0)
            return null;

        var roll = Random.Range(0, totalChance);

        foreach (var config in EnvironmentConfigs)
        {
            if (roll < config.Chance)
                return config;
            
            roll -= config.Chance;
        }

        return null;
    }
}
}