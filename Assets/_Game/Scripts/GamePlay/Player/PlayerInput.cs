using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Movement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerInput : ITickable
{
    private IMovementController _movement;
    private IAttackController _attack;
    private CameraController _camera;

    [Inject]
    private void Construct(IMovementController movement, IAttackController attack, CameraController cameraController)
    {
        _movement = movement;
        _attack = attack;
        _camera = cameraController;
    }

    public void Tick()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _movement.SetDirection(direction);
    }

    private void HandleAttack()
    {
        if (!Input.GetMouseButton(0)) return;

        _attack.RequestAttack(null, GetMouseWorldPosition());
    }

    private Vector2 GetMouseWorldPosition()
    {
        return _camera.Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
}