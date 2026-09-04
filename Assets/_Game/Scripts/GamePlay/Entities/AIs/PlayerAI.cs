using System;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.AIs
{
public class PlayerAI : IStartable, ITickable, IDisposable
{
    [Inject] private IMovementController _movement;
    [Inject] private IAttackController _attack;
    [Inject] private IHealthController _health;
    [Inject] private BodyHitbox _hitBox;
    [Inject] private CameraController _camera;

    public void Start()
    {
        _hitBox.OnHit += TakeDamage;
    }

    private void TakeDamage(HitInfo hit)
    {
        _health.TakeDamage(hit);
    }

    public void Tick()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _movement.SetDirection(direction);
    }

    private void HandleAttack()
    {
        if (!Input.GetMouseButton(0)) return;

        _attack.RequestAttack(null, GetMouseWorldPosition());
    }

    private Vector2 GetMouseWorldPosition()
    {
        return _camera.Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Dispose()
    {
        _hitBox.OnHit -= TakeDamage;
    }
}
}