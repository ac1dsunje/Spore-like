using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class PlayerMouth: MonoBehaviour
{
    private EatStats _stats;
    
    public void Construct(EatStats stats)
    {
        _stats = stats;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryEat(other);
    }

    private void TryEat(Collider2D other)
    {
        other.TryGetComponent<FoodItem>(out var food);
        if (food == null) return;
        
        _stats.EatFood(food.Get());
        food.Release();
    }
}
}