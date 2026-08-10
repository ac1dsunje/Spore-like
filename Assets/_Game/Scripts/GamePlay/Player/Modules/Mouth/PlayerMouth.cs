using System.Collections;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Mouth
{
public class PlayerMouth: MonoBehaviour
{
    private MouthModule _module;
    private FoodItem _currentFood;
    
    public void Construct(MouthModule module)
    {
        _module = module;
    }

    private void OnTriggerEnter2D(Collider2D other) => TryCatchFood(other);

    private void OnTriggerExit2D(Collider2D other) => TryReleaseFood(other);

    private void TryCatchFood(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        _currentFood = food;
        _currentFood.OnDeath += OnFoodDeath;
        StartCoroutine(Eat(_currentFood));
    }

    private void TryReleaseFood(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        if (_currentFood == food) _currentFood.OnDeath -= OnFoodDeath;
        _currentFood = null;
        StopAllCoroutines();
    }

    private IEnumerator Eat(FoodItem food)
    {
        while (true)
        {
            yield return new WaitForSeconds(_module.EatingTime);
            food.TakeHit(_module.EatingStrength, _module.EatingPenetration);
        }
    }

    private void OnFoodDeath(int foodAmount)
    {
        _module.GetExperienceFromFood(foodAmount);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}