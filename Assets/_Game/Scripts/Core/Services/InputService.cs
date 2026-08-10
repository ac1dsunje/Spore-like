using UnityEngine;

namespace _Game.Scripts.Core.Services
{
public sealed class InputService : IInputService
{
    public Vector2 MousePosition => Input.mousePosition;

    public Vector2 MouseDelta => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    public float MouseScroll => Input.mouseScrollDelta.y;

    public bool IsLeftMousePressed => Input.GetMouseButton(0);

    public bool WasLeftMousePressed => Input.GetMouseButtonDown(0);

    public bool WasLeftMouseReleased => Input.GetMouseButtonUp(0);

    public bool IsRightMousePressed => Input.GetMouseButton(1);

    public bool WasRightMousePressed => Input.GetMouseButtonDown(1);

    public bool WasRightMouseReleased => Input.GetMouseButtonUp(1);

    public bool IsKeyPressed(KeyCode key) => Input.GetKey(key);

    public bool WasKeyPressed(KeyCode key) => Input.GetKeyDown(key);

    public bool WasKeyReleased(KeyCode key) => Input.GetKeyUp(key);
}
}