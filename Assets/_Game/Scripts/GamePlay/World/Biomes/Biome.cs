namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome
{
    public string Name => Config.name;
    public float Temperature => Config.Temperature;
    public float PassAbility => Config.PassAbility;
    
    public BiomeConfig Config { get; private set; }

    public Biome(BiomeConfig biomeConfig)
    {
        Config = biomeConfig;
    }
}
}