using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponHolder: MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    public void SetAttack(Vector2 mousePosition, Vector2 entityPosition, HitInfo hitInfo, float range, ProjectileConfig config)
    {
        var weapon = Instantiate(_projectilePrefab);
        UpdateAttackPosition(mousePosition, entityPosition, range, weapon, config);

        weapon.Initialize(config, transform);

        weapon.SetHit(hitInfo);
    }

    private void UpdateAttackPosition(Vector2 mousePosition, Vector2 entityPosition, float range, Projectile weapon, ProjectileConfig config)
    {
        var offset = mousePosition - entityPosition;
        var rawDistance = offset.magnitude;

        var distance = Mathf.Clamp(rawDistance, config.MinRange, config.MaxRange > range? range: config.MaxRange);

        var direction = rawDistance > Mathf.Epsilon ? offset.normalized : Vector2.right;

        weapon.transform.position = entityPosition + direction * distance;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}