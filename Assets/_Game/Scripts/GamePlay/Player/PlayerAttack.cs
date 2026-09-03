using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerAttack : MonoBehaviour, IDamageSource, IDamageSourceController, IAttackController
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    
    private AttackModule _attack;
    private MovementModule _movement;
    private IDamageReceiver _receiver;

    private float _attackCooldownTimer;
    private bool CanAttack => _attackCooldownTimer <= 0f;

    [Inject]
    private void Construct(AttackModule attack, MovementModule movement)
    {
        _attack = attack;
        _movement = movement;
        _meleeWeaponObject.transform.SetParent(null);
    }

    public void SetDamageReceiver(IDamageReceiver damageReceiver) => _receiver = damageReceiver;

    private void Update()
    {
        if (_attackCooldownTimer > 0f)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }

    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 mousePosition)
    {
        if (!CanAttack) return;
        
        _meleeWeaponObject.gameObject.SetActive(true);
        UpdateAttackPosition(mousePosition);
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _meleeWeaponObject.SetHit(hit);
        
        _attackCooldownTimer = _attack.AttackSpeed;
    }
    
    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);

    private void UpdateAttackPosition(Vector2 mousePosition)
    {
        var playerPosition = (Vector2)_movement.Transform.position;

        var offset = mousePosition - playerPosition;
        var rawDistance = offset.magnitude;

        var distance = Mathf.Clamp(rawDistance, 0.5f, _attack.AttackRange);

        var direction = rawDistance > Mathf.Epsilon ? offset.normalized : Vector2.right;

        _meleeWeaponObject.transform.position = playerPosition + direction * distance;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _meleeWeaponObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}