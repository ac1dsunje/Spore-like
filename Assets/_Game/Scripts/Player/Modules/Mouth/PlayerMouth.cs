using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class PlayerMouth: MonoBehaviour
{
    private EatModule _module;
    
    public void Construct(EatModule module)
    {
        _module = module;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryEat(other);
    }

    private void TryEat(Collider2D other)
    {
        other.TryGetComponent<FoodItem>(out var food);
        if (food == null) return;
        
        _module.EatFood(food.Get());
        food.Release();
    }
}
}