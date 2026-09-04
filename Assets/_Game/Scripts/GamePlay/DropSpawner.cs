using _Game.Scripts.GamePlay.Entities.Drops;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class DropSpawner: MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField, Range(0.1f, 5f)] private float _spawnRadius; 
    
    public void Spawn(int amount, Vector2 position, DropsConfig dropConfigs)
    {
        for (var i = 0; i < amount; i++)
        {
            var randomOffset = Random.insideUnitCircle * _spawnRadius;
            
            var spawnPosition = position + new Vector2(randomOffset.x, randomOffset.y);
            
            var drop = Instantiate(_dropPrefab, spawnPosition, Quaternion.identity, transform);

            var config = dropConfigs.Drops[Random.Range(0, dropConfigs.Drops.Count)];
            
            drop.SetConfig(config);
        }
    }
}
}