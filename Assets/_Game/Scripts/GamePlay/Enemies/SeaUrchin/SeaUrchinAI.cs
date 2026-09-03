using System;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinAI : IStartable, ITickable, IDisposable
{
    [Inject] private IMovementController _movement;
    [Inject] private IAttackController _attacker;
    [Inject] private BodyHitbox _hitbox;

    private float _directionChangeTimer;

    private const float MinDirectionChangeTime = 0.5f;
    private const float MaxDirectionChangeTime = 2f;

    public void Start()
    {
        _hitbox.OnDamageReceiver += DoDamage;
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
        _hitbox.OnDamageReceiver -= DoDamage;
    }
}
}