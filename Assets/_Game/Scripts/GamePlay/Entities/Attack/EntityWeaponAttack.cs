using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponAttack : IDamageSource, IAttackController, IDisposable
{
    [Inject] private AttackModule _attack;
    [Inject] private IDamageReceiver _receiver;
    [Inject] private EntityWeaponHolder _weapon;
    [Inject] private ProjectileConfig _projectileConfig;
    [Inject] private CoroutineRunner _coroutineRunner;

    private const string CooldownKey = "Attack cooldown";

    private bool CanAttack => !_coroutineRunner.IsRunning(this, CooldownKey) && _attack.AttackTime > 0f;

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 targetPosition)
    {
        if (!CanAttack) return;
        
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _weapon.SetAttack(targetPosition, hit, _projectileConfig);
        
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