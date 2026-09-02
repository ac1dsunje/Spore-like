using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerMouth: IStartable, ITickable, IDisposable
{
    [Inject] private MouthModule _mouth;
    [Inject] private StomachModule _stomach;
    [Inject] private BuffsModule _buffs;
    [Inject] private BodyHitbox _hitbox;
    [Inject] private CoroutineRunner _coroutineRunner;

    private readonly HashSet<IBiteable> _foods = new();
    private IBiteable _currentFood;

    private float _hungerTimer;
    private float _maxTime = 30f;

    private const string CoroutineKey = "Eating";

    public void Start()
    {
        _hungerTimer = _maxTime;
        _hitbox.OnBiteAbleEntered += TryCatchFood;
        _hitbox.OnBiteAbleExited += TryReleaseFood;
    }

    private void TryCatchFood(IBiteable food)
    {
        if (!_foods.Add(food)) return;

        food.OnEaten += OnFoodEaten;

        if (_currentFood == null)
        {
            StartEatingNextFood();
        }
    }

    private void TryReleaseFood(IBiteable food)
    {
        if (!_foods.Remove(food)) return;

        food.OnEaten -= OnFoodEaten;

        if (_currentFood == food)
        {
            _currentFood = null;
            _coroutineRunner.Stop(CoroutineKey);
            StartEatingNextFood();
        }
    }

    private void StartEatingNextFood()
    {
        if (_currentFood != null) return;

        foreach (var food in _foods)
        {
            _currentFood = food;
            _coroutineRunner.Run(CoroutineKey, Eat(food));
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

        _coroutineRunner.Stop(CoroutineKey);
        StartEatingNextFood();
    }

    public void Tick()
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

    public void Dispose()
    {
        foreach (var food in _foods)
        {
            food.OnEaten -= OnFoodEaten;
        }

        _foods.Clear();
        _coroutineRunner.Stop(CoroutineKey);
        
        _hitbox.OnBiteAbleEntered -= TryCatchFood;
        _hitbox.OnBiteAbleExited -= TryReleaseFood;
    }
}
}