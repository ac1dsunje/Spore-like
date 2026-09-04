using System;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace _Game.Scripts.GamePlay.Entities.AIs
{
public class SeaUrchinAI : IStartable, ITickable, IDisposable
{
    [Inject] private IMovementController _movement;
    [Inject] private IAttackController _attacker;
    [Inject] private IHealthController _healthController;
    [Inject] private BodyHitbox _hitBox;

    private float _directionChangeTimer;

    private const float MinDirectionChangeTime = 0.5f;
    private const float MaxDirectionChangeTime = 2f;

    public void Start()
    {
        _hitBox.OnDamageReceiver += DoDamage;
        _hitBox.OnHit += TakeDamage;
    }

    private void TakeDamage(HitInfo hit)
    {
        _healthController.TakeDamage(hit);
    }

    private void DoDamage(IDamageReceiver damageReceiver)
    {
        _attacker.RequestAttack(damageReceiver, Vector2.zero);
    }

    public void Tick()
    {
        _directionChangeTimer -= Time.deltaTime;

        if (_directionChangeTimer > 0f)
            return;

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        var direction = Random.insideUnitCircle.normalized;

        if (direction.sqrMagnitude <= Mathf.Epsilon) direction = Vector2.right;

        _movement.SetDirection(direction);

        _directionChangeTimer = Random.Range(MinDirectionChangeTime, MaxDirectionChangeTime);
    }

    public void Dispose()
    {
        _hitBox.OnDamageReceiver -= DoDamage;
        _hitBox.OnHit -= TakeDamage;
    }
}
}