using _Game.Scripts.GamePlay.World.Biome;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World
{
public class WorldModel
{
    public int ChunkSize => _config.ChunkSize;
    public int BiomeCount => _config.BiomeConfigs.Count;
    
    private readonly WorldGenerationConfig _config;
    private readonly float _seed;

    public WorldModel(WorldGenerationConfig config)
    {
        _config = config;
        _seed = _config.GenerateRandomSeed ? Random.Range(0, 99999) : _config.Seed;
    }

    public BiomeConfig GetBiome(Vector3Int position)
    {
        var x = (position.x + _seed * 1000f) * _config.Scale;
        var y = (position.y + _seed * 1000f) * _config.Scale;
    
        var noiseValue = Mathf.PerlinNoise(x, y);
    
        var biomeCount = BiomeCount;
        var biomeIndex = Mathf.FloorToInt(noiseValue * biomeCount);
    
        biomeIndex = Mathf.Clamp(biomeIndex, 0, biomeCount - 1);
    
        return _config.BiomeConfigs[biomeIndex];
    }
}
}