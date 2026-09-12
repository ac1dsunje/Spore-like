using System;
using System.Threading;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Projectiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponAttack : IDamageSource, IAttackController, IDisposable
{
    private readonly AttackModule _attack;
    private readonly IDamageReceiver _receiver;
    private readonly ProjectileSpawner _projectileSpawner;
    private readonly Transform _transform;
    private readonly ProjectileConfig _projectileConfig;

    private readonly CancellationTokenSource _lifetimeCts = new();
    
    private bool _isOnCooldown;

    private bool CanAttack => !_isOnCooldown && _attack.AttackTime > 0f;

    public EntityWeaponAttack(AttackModule attack, IDamageReceiver receiver, ProjectileSpawner projectileSpawner,
        Transform transform, ProjectileConfig projectileConfig)
    {
        _attack = attack;
        _receiver = receiver;
        _projectileSpawner = projectileSpawner;
        _transform = transform;
        _projectileConfig = projectileConfig;
    }

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 targetPosition)
    {
        if (!CanAttack) return;
        
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _projectileSpawner.SetAttack(targetPosition, _transform, hit, _projectileConfig);
        
        _isOnCooldown = true;
        CooldownAsync().Forget();
    }

    private async UniTaskVoid CooldownAsync()
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_attack.AttackTime), cancellationToken: _lifetimeCts.Token);
        }
        catch (OperationCanceledException)
        {
            
        }
        finally
        {
            _isOnCooldown = false;
        }
    }
    
    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);

    public void Dispose()
    {
        _lifetimeCts.Cancel();
        _lifetimeCts.Dispose();
    }
}
}