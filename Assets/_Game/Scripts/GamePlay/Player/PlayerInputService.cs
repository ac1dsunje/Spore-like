using _Game.Scripts.Core.Services;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerInputService
{
    private readonly IInputService _input;

    [Inject]
    public PlayerInputService(IInputService input)
    {
        _input = input;
    }

    public float Horizontal =>
        (_input.IsKeyPressed(KeyCode.D) ? 1f : 0f) -
        (_input.IsKeyPressed(KeyCode.A) ? 1f : 0f);

    public float Vertical =>
        (_input.IsKeyPressed(KeyCode.W) ? 1f : 0f) -
        (_input.IsKeyPressed(KeyCode.S) ? 1f : 0f);

    public Vector2 Movement => new(Horizontal, Vertical);

    public bool AttackPressed => _input.WasLeftMousePressed;

    public Vector2 MousePosition => _input.MousePosition;
}
}