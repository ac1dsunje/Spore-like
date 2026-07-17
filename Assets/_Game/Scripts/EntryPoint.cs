using _Game.Scripts.Camera;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private PlayerController _player;

    private void Awake()
    {
        _camera.Construct(_player.transform);
    }
}
}
