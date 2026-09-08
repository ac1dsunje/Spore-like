using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponHolder: MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    public void SetAttack(Vector2 targetPosition, HitInfo hitInfo, ProjectileConfig config)
    {
        var weapon = Instantiate(_projectilePrefab);
        weapon.transform.localScale = Vector3.one;
        UpdateAttackPosition(targetPosition, transform.position, weapon, config);

        weapon.Initialize(config, transform);

        weapon.SetHit(hitInfo);
    }

    private void UpdateAttackPosition(Vector2 mousePosition, Vector2 entityPosition, Projectile weapon, ProjectileConfig config)
    {
        var offset = mousePosition - entityPosition;
    
        var direction = offset.sqrMagnitude > Mathf.Epsilon ? offset.normalized : Vector2.right;

        weapon.transform.position = entityPosition + direction * config.OffsetStartPoint;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}