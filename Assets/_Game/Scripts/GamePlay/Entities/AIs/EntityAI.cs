using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace _Game.Scripts.GamePlay.Entities.AIs
{
public class EntityAI : IStartable, IDisposable
{
    private readonly IMovementController _movement;
    private readonly IAttackController _attacker;
    private readonly IHealthController _healthController;
    private readonly BodyHitbox _hitBox;
    private readonly EvolutionsModule _evolutions;
    private readonly ExperienceModule _experience;
    
    private readonly Transform _transform;
    private readonly EntitiesRegistry _entitiesRegistry;

    private CancellationTokenSource _cts;

    public EntityAI(IMovementController movement, IAttackController attacker, IHealthController healthController,
        BodyHitbox hitbox, EvolutionsModule evolutions, ExperienceModule experience, 
        Transform transform, EntitiesRegistry entitiesRegistry)
    {
        _movement = movement;
        _attacker = attacker;
        _healthController = healthController;
        _hitBox = hitbox;
        _evolutions = evolutions;
        _experience = experience;
        _transform = transform;
        _entitiesRegistry = entitiesRegistry;
    }

    public void Start()
    {
        _hitBox.OnTouch += DoDamage;
        _hitBox.OnHit += TakeDamage;
        _evolutions.OnSlotsFilled += ChooseEvolution;
        
        SetInitialEvolutions(_experience.Level);

        _cts = new CancellationTokenSource();
        DirectionChangeLoopAsync(_cts.Token).Forget();
    }

    private void SetInitialEvolutions(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var availableEvolutions = _evolutions.Evolutions
                .Where(e => e.State == EvolutionState.IsAble)
                .ToList();

            if (availableEvolutions.Count == 0) break; 

            var randomIndex = Random.Range(0, availableEvolutions.Count);
            _evolutions.ChooseEvolution(availableEvolutions[randomIndex]);
        }
    }

    private async UniTaskVoid DirectionChangeLoopAsync(CancellationToken token)
    {
        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                
                ChangeDirection();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void ChooseEvolution(List<Evolution> evolutions)
    {
        var evolution = evolutions[Random.Range(0, evolutions.Count)];
        _evolutions.ChooseEvolution(evolution);
    }

    private void TakeDamage(HitInfo hit)
    {
        _healthController.TakeDamage(hit);
    }

    private void DoDamage(IDamageReceiver damageReceiver)
    {
        _attacker.RequestAttack(damageReceiver, Vector2.zero);
    }

    private void ChangeDirection()
    {
        Vector2 direction;
        var chasingEntity = _entitiesRegistry.Player;

        if (chasingEntity != null)
        {
            var playerPosition = chasingEntity.position;
            var creaturePosition = _transform.position;
            direction = (playerPosition - creaturePosition).normalized;
        }
        else
        {
            direction = Random.insideUnitCircle.normalized;
            if (direction.sqrMagnitude <= Mathf.Epsilon) 
                direction = Vector2.right;
        }

        _movement.SetDirection(direction);
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _hitBox.OnTouch -= DoDamage;
        _hitBox.OnHit -= TakeDamage;
        _evolutions.OnSlotsFilled -= ChooseEvolution;
    }
}
}