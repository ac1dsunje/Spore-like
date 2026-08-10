using System.Collections.Generic;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World
{
public class WorldModel
{
    public WorldGenerationConfig Config { get; private set; }
    private readonly float _seed;

    private readonly Dictionary<Vector3Int, int> _biomeIndices = new();

    public WorldModel(WorldGenerationConfig config)
    {
        Config = config;
        _seed = Config.GenerateRandomSeed ? Random.Range(0, 99999) : Config.Seed;
    }

    public BiomeConfig GetBiome(Vector3Int position)
    {
        if (_biomeIndices.TryGetValue(position, out var index))
        {
            return Config.BiomeConfigs[index];
        }
        
        var x = (position.x + _seed * 1000f) * Config.Scale;
        var y = (position.y + _seed * 1000f) * Config.Scale;
    
        var noiseValue = Mathf.PerlinNoise(x, y);
    
        var biomeCount = Config.BiomeConfigs.Count;
        var biomeIndex = Mathf.FloorToInt(noiseValue * biomeCount);
    
        biomeIndex = Mathf.Clamp(biomeIndex, 0, biomeCount - 1);
        
        _biomeIndices.Add(position, biomeIndex);
    
        return Config.BiomeConfigs[biomeIndex];
    }
}
}