using UnityEngine;

namespace _Game.Scripts.Core.Services
{
public interface IInputService
{
    Vector2 MousePosition { get; }
    Vector2 MouseDelta { get; }
    float MouseScroll { get; }

    bool IsLeftMousePressed { get; }
    bool WasLeftMousePressed { get; }
    bool WasLeftMouseReleased { get; }

    bool IsRightMousePressed { get; }
    bool WasRightMousePressed { get; }
    bool WasRightMouseReleased { get; }

    bool IsKeyPressed(KeyCode key);
    bool WasKeyPressed(KeyCode key);
    bool WasKeyReleased(KeyCode key);
}
}