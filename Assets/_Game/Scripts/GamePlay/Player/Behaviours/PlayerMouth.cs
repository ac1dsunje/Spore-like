using System.Collections;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMouth: PlayerNetworkBehaviour
{
    private MouthModule _module;
    private FoodItem _currentFood;
    
    [Inject]
    private void Construct(MouthModule module)
    {
        _module = module;
    }

    private void OnTriggerEnter2D(Collider2D other) => TryCatchFood(other);

    private void OnTriggerExit2D(Collider2D other) => TryReleaseFood(other);

    private void TryCatchFood(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        StopAllCoroutines();
        _currentFood = food;
        _currentFood.OnDeath += OnFoodDeath;
        StartCoroutine(Eat(_currentFood));
    }

    private void TryReleaseFood(Collider2D other)
    {
        if (!other.TryGetComponent<FoodItem>(out var food)) return;
        StopAllCoroutines();
        if (_currentFood == null) return;
        _currentFood.OnDeath -= OnFoodDeath;
        _currentFood = null;
    }

    private IEnumerator Eat(FoodItem food)
    {
        while (_currentFood)
        {
            yield return new WaitForSeconds(_module.EatingTime);
            food?.TakeHit(_module.EatingStrength, _module.EatingPenetration);
        }
    }

    private void OnFoodDeath(int foodAmount)
    {
        _module.GetExperienceFromFood(foodAmount);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}
}