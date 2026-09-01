using _Game.Scripts.GamePlay.Entities;

namespace _Game.Scripts.GamePlay.Enemies
{
public class EnemiesSpawner: EntitySpawner
{
    private void Awake()
    {
        Spawn();
    }
}
}