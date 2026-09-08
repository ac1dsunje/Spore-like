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
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace _Game.Scripts.GamePlay.Entities.AIs
{
public class EntityAI : IStartable, IDisposable
{
    [Inject] private IMovementController _movement;
    [Inject] private IAttackController _attacker;
    [Inject] private IHealthController _healthController;
    [Inject] private BodyHitbox _hitBox;
    [Inject] private EvolutionsModule _evolutions;
    [Inject] private EntityModel _model;
    [Inject] private CoroutineRunner _coroutineRunner;
    [Inject] private ExperienceModule _experience;

    private const string DirectionChangeKey = "DirectionChange";

    private const float MinDirectionChangeTime = 0.5f;
    private const float MaxDirectionChangeTime = 2f;

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