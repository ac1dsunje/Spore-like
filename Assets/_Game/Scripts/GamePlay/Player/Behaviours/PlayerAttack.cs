using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerAttack : MonoBehaviour, IDamageSource, IDamageSourceController
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    
    private AttackModule _attack;
    private MovementModule _movement;
    private Ticker _ticker;
    private IDamageReceiver _receiver;
    private CameraController _camera;

    private float _attackCooldownTimer;
    private bool CanAttack => _attackCooldownTimer <= 0f;

    [Inject]
    private void Construct(AttackModule attack, MovementModule movement, Ticker ticker, CameraController cameraController)
    {
        _attack = attack;
        _movement = movement;
        _camera = cameraController;
        _meleeWeaponObject.transform.SetParent(null);
        _ticker = ticker;
        _ticker.OnTick += CheckInput;
    }

    public void SetDamageReceiver(IDamageReceiver damageReceiver) => _receiver = damageReceiver;

    private void CheckInput(float timeDelta)
    {
        if (_attackCooldownTimer > 0f)
        {
            _attackCooldownTimer -= timeDelta;
        }

        if (!Input.GetMouseButton(0)) return;
        Attack();
    }

    private void Attack()
    {
        if (!CanAttack) return;
        
        _meleeWeaponObject.gameObject.SetActive(true);
        UpdateAttackPosition();
        var hit = new HitInfo(_attack.PhysicalDamage, _attack.IgnoreResistance, this, _receiver);
        _meleeWeaponObject.SetHit(hit);
        
        _attackCooldownTimer = _attack.AttackSpeed;
    }
    
    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);

    private void UpdateAttackPosition()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = Mathf.Abs(_camera.transform.position.z);
        
        var mouseWorld = (Vector2)_camera.Camera.ScreenToWorldPoint(screenPoint);
        var playerPosition = (Vector2)_movement.Transform.position;

        var offset = mouseWorld - playerPosition;
        var rawDistance = offset.magnitude;

        var distance = Mathf.Clamp(rawDistance, 0.5f, _attack.AttackRange);

        var direction = rawDistance > Mathf.Epsilon ? offset.normalized : Vector2.right;

        _meleeWeaponObject.transform.position = playerPosition + direction * distance;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _meleeWeaponObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}