using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attackObject;
    
    private AttackModule _module;

    public void Construct(AttackModule module)
    {
        _module = module;
    }

    private void Update()
    {
        UpdateAttackPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void UpdateAttackPosition()
    {
        var mouseWorldPosition = Input.mousePosition;
        mouseWorldPosition.z = transform.position.z;

        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        _attackObject.transform.localPosition = direction * _module.AttackRange;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _attackObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Attack()
    {
        Debug.Log($"Attack with {_module.PhysicalDamage} damage, range {_module.AttackRange}");
    }
}
}