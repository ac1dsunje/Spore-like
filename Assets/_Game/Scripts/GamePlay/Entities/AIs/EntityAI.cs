using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Services;
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
    private readonly EntityModel _model;
    private readonly CoroutineRunner _coroutineRunner;
    private readonly ExperienceModule _experience;

    private const string DirectionChangeKey = "DirectionChange";

    private const float MinDirectionChangeTime = 0.5f;
    private const float MaxDirectionChangeTime = 2f;

    public EntityAI(IMovementController movement, IAttackController attacker, IHealthController healthController,
        BodyHitbox hitbox, EvolutionsModule evolutions, EntityModel model, CoroutineRunner coroutineRunner, 
        ExperienceModule experience)
    {
        _movement = movement;
        _attacker = attacker;
        _healthController = healthController;
        _hitBox = hitbox;
        _evolutions = evolutions;
        _model = model;
        _coroutineRunner = coroutineRunner;
        _experience = experience;
    }

    public void Start()
    {
        _hitBox.OnTouch += DoDamage;
        _hitBox.OnHit += TakeDamage;
        _evolutions.OnSlotsFilled += ChooseEvolution;
        
        SetInitialEvolutions(_experience.Level);

        _coroutineRunner.Run(this, DirectionChangeKey, DirectionChangeRoutine());
    }

    private void SetInitialEvolutions(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var availableEvolutions = _evolutions.Evolutions
                .Where(e => e.State == EvolutionState.IsAble)
                .ToList();

            if (availableEvolutions.Count == 0)
            {
                break; 
            }

            var randomIndex = Random.Range(0, availableEvolutions.Count);
            _evolutions.ChooseEvolution(availableEvolutions[randomIndex]);
        }
    }

    private IEnumerator DirectionChangeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinDirectionChangeTime, MaxDirectionChangeTime));
            ChangeDirection();
        }
    }

    private void ChooseEvolution(List<Evolution> evolutions)
    {
        var evolution = evolutions[Random.Range(0, evolutions.Count)];
        evolution.Apply(_model);
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
        EntityModel chasingEntity = null;

        if (_model.Vision.CanSee())
            chasingEntity = _model.Social.GetEntityWithHighestInfluence();
    
        if (chasingEntity != null)
        {
            var playerPosition = chasingEntity.Movement.Transform.position;
            var creaturePosition = _model.Movement.Transform.position;
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
        _coroutineRunner.Stop(this);
        _hitBox.OnTouch -= DoDamage;
        _hitBox.OnHit -= TakeDamage;
        _evolutions.OnSlotsFilled -= ChooseEvolution;
    }
}
}