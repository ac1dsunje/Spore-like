using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome: IStatSource
{
    public string Name => _config.name;
    public float Temperature => _config.Temperature;
    public float PassAbility => _config.PassAbility;
    public TileBase Tile => _config.Tile;
    public float ChanceEnvironment => _config.ChanceEnvironment;
    public float OxygenBreathing => _config.OxygenBreathing;
    public float HydrogenBreathing => _config.HydrogenBreathing;
    
    public List<SourceStat> GetStats() => _config.AffectedStats;
    
    private readonly BiomeConfig _config;

    public Biome(BiomeConfig biomeConfig)
    {
        _config = biomeConfig;
    }
    
    public EnvironmentConfig GetRandomEnvironment()
    {
        var environments = _config.EnvironmentConfigs;
        
        if (environments.Count == 0) return null;
        
        var totalChance = environments.Sum(config => config.Chance);

        if (totalChance <= 0) return null;

        var roll = Random.Range(0, totalChance);

        foreach (var config in environments)
        {
            if (roll < config.Chance) return config;
            
            roll -= config.Chance;
        }

        return null;
    }
}
}