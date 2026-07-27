using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.World.Biome
{
[CreateAssetMenu(fileName = "New Biome Config", menuName = "Configs/Game/World/Biomes/Biome")]
public class BiomeConfig: ScriptableObject
{
    [field: SerializeField] private List<TileBase> _tiles;
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public List<EnvironmentConfig> EnvironmentConfigs { get; private set; }
    [field: SerializeField] public int ChanceEnvironment { get; private set; } = 20;

    public TileBase RandomTile => _tiles[Random.Range(0, _tiles.Count)];

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