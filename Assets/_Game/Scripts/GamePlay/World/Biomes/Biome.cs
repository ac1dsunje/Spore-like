using System.Collections.Generic;

namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome: IStatSource
{
    public string Name => Config.name;
    public float Temperature => Config.Temperature;
    public float PassAbility => Config.PassAbility;
    
    public List<Stat> GetStats() => Config.AffectedStats;
    
    public BiomeConfig Config { get; private set; }

    public Biome(BiomeConfig biomeConfig)
    {
        Config = biomeConfig;
    }
}
}