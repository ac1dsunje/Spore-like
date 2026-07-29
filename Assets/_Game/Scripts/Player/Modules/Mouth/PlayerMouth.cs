using System.Collections;
using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class PlayerMouth: MonoBehaviour
{
    private EatModule _module;
    private FoodItem _currentFood;
    
    public void Construct(EatModule module)
    {
        _module = module;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryCatchFood(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        _currentFood = null;
        StopAllCoroutines();
    }

    private void TryCatchFood(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        _currentFood = food;
        StartCoroutine(Eat(_currentFood));
    }

    private IEnumerator Eat(FoodItem food)
    {
        while (food.IsAlive)
        {
            yield return new WaitForSeconds(1f);
            food.TakeHit(_module.EatingStrength);
        }
        
        _module.GetExperienceFromFood(food.FeedAmount);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}