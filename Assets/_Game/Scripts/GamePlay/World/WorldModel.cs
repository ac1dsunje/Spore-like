using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World
{
public class WorldModel
{
    public int ChunkSize => _config.ChunkSize;
    
    private readonly WorldGenerationConfig _config;
    private readonly float _seed;

    private readonly Dictionary<Vector3Int, int> _biomeIndices = new();

    private readonly List<Biome> _biomes = new();

    public WorldModel(WorldGenerationConfig config)
    {
        _config = config;
        _seed = _config.GenerateRandomSeed ? Random.Range(0, 99999) : _config.Seed;

        foreach (var newBiome in _config.BiomeConfigs.Select(biomeConfig => new Biome(biomeConfig)))
        {
            _biomes.Add(newBiome);
        }
    }

    public List<Biome> GetBiomes()
    {
        return _biomes;
    }

    public Biome GetBiome(Vector3Int position)
    {
        if (_biomeIndices.TryGetValue(position, out var index))
        {
            return _biomes[index];
        }
        
        var x = (position.x + _seed * 1000f) * _config.Scale;
        var y = (position.y + _seed * 1000f) * _config.Scale;
    
        var noiseValue = Mathf.PerlinNoise(x, y);
    
        var biomeCount = _config.BiomeConfigs.Count;
        var biomeIndex = Mathf.FloorToInt(noiseValue * biomeCount);
    
        biomeIndex = Mathf.Clamp(biomeIndex, 0, biomeCount - 1);
        
        _biomeIndices[position] = biomeIndex;
    
        return _biomes[biomeIndex];
    }
}
}