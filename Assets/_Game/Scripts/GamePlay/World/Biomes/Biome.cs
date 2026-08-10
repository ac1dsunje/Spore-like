using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes
{
public class Biome: MonoBehaviour
{
    private BiomeConfig _config;

    public void Construct(BiomeConfig biomeConfig)
    {
        _config = biomeConfig;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //ToDo: add new interface for biome addicted 
        if (other.TryGetComponent(out IDamageAble damageAble))
        {
            Debug.Log($"{damageAble.GetType().Name} entered to {_config.name}");
        }
    }
}
}