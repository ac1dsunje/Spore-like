using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public class EntityWeaponHolder: MonoBehaviour
{
    [SerializeField] private MeleeWeaponItem _meleeWeaponObject;
    private void Awake()
    {
        _meleeWeaponObject.transform.SetParent(null);
    }

    public void SetAttack(Vector2 mousePosition, Vector2 entityPosition, HitInfo hitInfo, float range)
    {
        _meleeWeaponObject.gameObject.SetActive(true);
        UpdateAttackPosition(mousePosition, entityPosition, range);
        _meleeWeaponObject.SetHit(hitInfo);
    }

    private void UpdateAttackPosition(Vector2 mousePosition, Vector2 entityPosition, float range)
    {
        var offset = mousePosition - entityPosition;
        var rawDistance = offset.magnitude;

        var distance = Mathf.Clamp(rawDistance, 0.5f, range);

        var direction = rawDistance > Mathf.Epsilon ? offset.normalized : Vector2.right;

        _meleeWeaponObject.transform.position = entityPosition + direction * distance;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _meleeWeaponObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}