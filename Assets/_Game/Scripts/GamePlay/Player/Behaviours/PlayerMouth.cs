using System.Collections;
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
        StopAllCoroutines();
        _currentFood = food;
        _currentFood.OnEaten += OnFoodDeath;
        StartCoroutine(Eat(_currentFood));
    }

    private void TryReleaseFood(Collider2D other)
    {
        if (!other.TryGetComponent<IBiteable>(out var food)) return;
        StopAllCoroutines();
        if (_currentFood == null) return;
        _currentFood.OnEaten -= OnFoodDeath;
        _currentFood = null;
    }

    private IEnumerator Eat(IBiteable food)
    {
        while (_currentFood != null)
        {
            yield return new WaitForSeconds(_module.EatingTime);
            food?.TakeByte(_module.EatingStrength, _module.EatingPenetration);
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