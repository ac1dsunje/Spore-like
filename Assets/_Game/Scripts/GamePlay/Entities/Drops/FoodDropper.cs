using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
public class FoodDropper: MonoBehaviour
{
    [SerializeField] private FoodDrop _dropPrefab;
    [Inject] private FoodConfig _foodConfig;

    public void Spawn(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var food = Instantiate(_dropPrefab, transform.position, transform.rotation, null);
            food.SetSprite(_foodConfig.Sprite);
        }
    }
}
}