using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
public class FoodDropper: MonoBehaviour
{
    [SerializeField] private FoodDrop _dropPrefab;
    [SerializeField, Range(0.1f, 5f)] private float _spawnRadius; 
        
    [Inject] private FoodConfig _foodConfig;

    public void Spawn(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var randomOffset = Random.insideUnitCircle * _spawnRadius;
            
            var spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
            
            var food = Instantiate(_dropPrefab, spawnPosition, transform.rotation, null);
            food.SetSprite(_foodConfig.Sprite);
        }
    }
}
}