using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome: MonoBehaviour
{
    public float Temperature => _config.Temperature;
    
    private BiomeConfig _config;

    public void Construct(BiomeConfig biomeConfig)
    {
        _config = biomeConfig;
    }

    
    // ToDo : get biome by coordinates and not by colliders
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out IBiomeAddicted creature))
        {
            creature.EnterBiome(this);
        }
    }
}
}