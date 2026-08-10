using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AttackItem _attackObject;
    
    private AttackModule _module;
    private IInputService _inputService;

    public void Construct(AttackModule module, IInputService inputService)
    {
        _module = module;
        _inputService = inputService;
    }

    private void Update()
    {
        if (_inputService.WasLeftMousePressed)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _attackObject.gameObject.SetActive(true);
        UpdateAttackPosition();
        _attackObject.SetDamage(_module.PhysicalDamage);
    }

    private void UpdateAttackPosition()
    {
        var mousePosition = _inputService.MousePosition;
        Vector2 playerPosition = transform.position;
        var direction = (mousePosition - playerPosition).normalized;

        _attackObject.transform.localPosition = direction * _module.AttackRange;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _attackObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}