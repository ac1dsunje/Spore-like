using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMouth: MonoBehaviour
{
    [Inject] private MouthModule _mouth;
    [Inject] private StomachModule _stomach;
    [Inject] private BuffsModule _buffs;

    private readonly HashSet<IBiteable> _foods = new();
    private IBiteable _currentFood;

    private float _hungerTimer;
    private float _maxTime = 30f;

    private void Awake()
    {
        _hungerTimer = _maxTime;
    }

    private void OnTriggerEnter2D(Collider2D other) => TryCatchFood(other);

    private void OnTriggerExit2D(Collider2D other) => TryReleaseFood(other);

    private void TryCatchFood(Collider2D other)
    {
        if (!other.TryGetComponent<IBiteable>(out var food)) return;
        if (!_foods.Add(food)) return;

        food.OnEaten += OnFoodEaten;

        if (_currentFood == null)
        {
            StartEatingNextFood();
        }
    }

    private void TryReleaseFood(Collider2D other)
    {
        if (!other.TryGetComponent<IBiteable>(out var food)) return;
        if (!_foods.Remove(food)) return;

        food.OnEaten -= OnFoodEaten;

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
            yield return new WaitForSeconds(_mouth.EatingTime);

            if (_currentFood != food) yield break;

            food.TakeBite(_mouth.EatingStrength, _mouth.EatingPenetration);
        }
    }

    private void OnFoodEaten(int foodAmount)
    {
        _stomach.GetExperienceFromFood(foodAmount);

        if (foodAmount <= 0) return;

        if (_currentFood is not null)
        {
            _currentFood.OnEaten -= OnFoodEaten;
            _foods.Remove(_currentFood);
            _currentFood = null;
        }

        StopAllCoroutines();
        StartEatingNextFood();
    }

    private void Update()
    {
        if (_stomach.Hunger > 0)
        {
            _hungerTimer -= Time.deltaTime;
            if (_hungerTimer <= 0)
            {
                _stomach.LoseHunger(1);
                _hungerTimer = _maxTime;
            }
        }

        _buffs.Set(BuffType.Starvation, _stomach.Hunger <= 0);
        _buffs.Set(BuffType.Overeating, _stomach.Hunger > _stomach.MaxHunger);
    }

    private void OnDestroy()
    {
        foreach (var food in _foods)
        {
            food.OnEaten -= OnFoodEaten;
        }

        _foods.Clear();

        StopAllCoroutines();
    }
}
}