using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class ParticlesSpawner: MonoBehaviour
{
    public void Spawn(ParticleSystem prefab, Vector3 position, Color color)
    {
        var particles = Instantiate(prefab, position, Quaternion.identity, transform);

        var main = particles.main;
        main.startColor = color;
    }
}
}