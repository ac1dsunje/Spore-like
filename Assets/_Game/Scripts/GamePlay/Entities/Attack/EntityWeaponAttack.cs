using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponAttack : IDamageSource, IAttackController, IDisposable
{
    private readonly AttackModule _attack;
    private readonly IDamageReceiver _receiver;
    private readonly ProjectileSpawner _projectileSpawner;
    private readonly MovementModule _movement;
    private readonly ProjectileConfig _projectileConfig;
    private readonly CoroutineRunner _coroutineRunner;

    private const string CooldownKey = "Attack cooldown";

    private bool CanAttack => !_coroutineRunner.IsRunning(this, CooldownKey) && _attack.AttackTime > 0f;

    public EntityWeaponAttack(AttackModule attack, IDamageReceiver receiver, ProjectileSpawner projectileSpawner,
        MovementModule movement, ProjectileConfig projectileConfig, CoroutineRunner coroutineRunner)
    {
        _attack = attack;
        _receiver = receiver;
        _projectileSpawner = projectileSpawner;
        _movement = movement;
        _projectileConfig = projectileConfig;
        _coroutineRunner = coroutineRunner;
    }

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 targetPosition)
    {
        if (!CanAttack) return;
        
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _projectileSpawner.SetAttack(targetPosition, _movement.Transform, hit, _projectileConfig);
        
        _coroutineRunner.Run(this, CooldownKey, CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(_attack.AttackTime);
    }
    
    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);

    public void Dispose()
    {
        _coroutineRunner.Stop(this);
    }
}
}