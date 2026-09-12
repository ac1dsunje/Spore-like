using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities.Configuration;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome
{
    public string Name => _config.name;
    public float Temperature => _config.Temperature;
    public float PassAbility => _config.PassAbility;
    public TileBase Tile => _config.Tile;
    public float ChanceEnvironment => _config.ChanceEnvironment;
    public float OxygenBreathing => _config.OxygenBreathing;
    public float HydrogenBreathing => _config.HydrogenBreathing;
    public List<EntityConfig> Enemies => _config.Enemies;
    
    private readonly BiomeConfig _config;

    public Biome(BiomeConfig biomeConfig)
    {
        _config = biomeConfig;
    }
}
}