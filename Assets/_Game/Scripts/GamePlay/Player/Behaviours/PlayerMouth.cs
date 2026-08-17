using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Network;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMouth: EntityNetworkBehaviour
{
    private MouthModule _module;

    private readonly HashSet<IBiteable> _foods = new();
    private IBiteable _currentFood;
    
    [Inject]
    private void Construct(MouthModule module)
    {
        _module = module;
    }

    private void OnTriggerEnter2D(Collider2D other) => TryCatchFood(other);

    private void OnTriggerExit2D(Collider2D other) => TryReleaseFood(other);

    private void TryCatchFood(Collider2D other)
    {
        if (!other.TryGetComponent<IBiteable>(out var food)) return;
        if (!_foods.Add(food)) return;

        food.OnEaten += OnFoodDeath;

        if (_currentFood == null)
        {
            StartEatingNextFood();
        }
    }

    private void TryReleaseFood(Collider2D other)
    {
        if (!other.TryGetComponent<IBiteable>(out var food)) return;
        if (!_foods.Remove(food)) return;

        food.OnEaten -= OnFoodDeath;

        if (_currentFood == food)
        {
            _currentFood = null;
            StopAllCoroutines();
            StartEatingNextFood();
        }
    }

    private void StartEatingNextFood()
    {
        if (_currentFood != null) return;

        foreach (var food in _foods)
        {
            _currentFood = food;
            StartCoroutine(Eat(food));
            break;
        }
    }

    private IEnumerator Eat(IBiteable food)
    {
        while (_currentFood == food && _foods.Contains(food))
        {
            yield return new WaitForSeconds(_module.EatingTime);

            if (_currentFood != food) yield break;

            food.TakeByte(_module.EatingStrength, _module.EatingPenetration);
        }
    }

    private void OnFoodDeath(int foodAmount)
    {
        _module.GetExperienceFromFood(foodAmount);

        if (foodAmount <= 0) return;

        if (_currentFood is not null)
        {
            _currentFood.OnEaten -= OnFoodDeath;
            _foods.Remove(_currentFood);
            _currentFood = null;
        }

        StopAllCoroutines();
        StartEatingNextFood();
    }

    protected override void OnDestroy()
    {
        foreach (var food in _foods)
        {
            food.OnEaten -= OnFoodDeath;
        }

        _foods.Clear();

        StopAllCoroutines();
        base.OnDestroy();
    }
}
}