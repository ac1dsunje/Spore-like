using _Game.Scripts.Core.Services;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerInputService
{
    private readonly IInputService _input;
    private readonly Camera _camera;

    [Inject]
    public PlayerInputService(IInputService input, Camera camera)
    {
        _input = input;
        _camera = camera;
    }

    public float Horizontal =>
        (_input.IsKeyPressed(KeyCode.D) ? 1f : 0f) -
        (_input.IsKeyPressed(KeyCode.A) ? 1f : 0f);

    public float Vertical =>
        (_input.IsKeyPressed(KeyCode.W) ? 1f : 0f) -
        (_input.IsKeyPressed(KeyCode.S) ? 1f : 0f);

    public Vector2 Movement => new(Horizontal, Vertical);

    public bool AttackPressed => _input.WasLeftMousePressed;

    public Vector2 MouseScreenPosition => _input.MousePosition;

    public Vector2 MouseWorldPosition
    {
        get
        {
            Vector3 screenPoint = _input.MousePosition;
            screenPoint.z = Mathf.Abs(_camera.transform.position.z);

            return _camera.ScreenToWorldPoint(screenPoint);
        }
    }
}
}