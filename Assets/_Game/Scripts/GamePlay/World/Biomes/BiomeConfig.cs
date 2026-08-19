using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[CreateAssetMenu(fileName = "New Biome Config", menuName = "Configs/Game/World/Biomes/Biome")]
public class BiomeConfig: ScriptableObject
{
    [field: SerializeField] public float Temperature { get; private set; }
    [field: SerializeField] public float PassAbility { get; private set; }
    [field: SerializeField] public List<SourceStat> AffectedStats { get; private set; } = new();
    [field: SerializeField] public TileBase Tile { get; private set; }
    [field: SerializeField] public List<EnvironmentConfig> EnvironmentConfigs { get; private set; }
    [field: SerializeField] public int ChanceEnvironment { get; private set; } = 20;

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