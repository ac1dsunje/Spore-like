using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    public void SetAttack(Vector2 targetPosition, Transform entity, HitInfo hitInfo, ProjectileConfig config)
    {
        var weapon = Instantiate(_projectilePrefab);
        var direction = GetDirection(targetPosition, entity.position);
        
        weapon.transform.localScale = Vector3.one;
        weapon.transform.position = new Vector2(entity.position.x, entity.position.y) + direction * config.OffsetStartPoint;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        weapon.Initialize(config, entity, hitInfo);
    }

    private Vector2 GetDirection(Vector2 mousePosition, Vector2 entityPosition)
    {
        var offset = mousePosition - entityPosition;
    
        return offset.sqrMagnitude > Mathf.Epsilon ? offset.normalized : Vector2.right;
    }
}
}