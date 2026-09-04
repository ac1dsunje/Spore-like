using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
public class FoodDropper: MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField, Range(0.1f, 5f)] private float _spawnRadius; 
        
    [Inject] private DropsConfig _dropConfigs;

    public void Spawn(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var randomOffset = Random.insideUnitCircle * _spawnRadius;
            
            var spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
            
            var drop = Instantiate(_dropPrefab, spawnPosition, transform.rotation, null);

            var config = _dropConfigs.Drops[Random.Range(0, _dropConfigs.Drops.Count)];
            
            drop.SetConfig(config);
        }
    }
}
}