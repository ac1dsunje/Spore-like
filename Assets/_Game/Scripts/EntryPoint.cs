using _Game.Scripts.Camera;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI;
using _Game.Scripts.Player;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [Header("UI")]
    [SerializeField] private CameraController _camera;
    [SerializeField] private OverlayScreen _overlayScreen;
    [SerializeField] private EvolutionChooseScreen _evolutionScreen;

    private void Awake()
    {
        _camera.Construct(_player.transform);
        _overlayScreen.Construct(_player);
        _evolutionScreen.Construct(_player);
    }
}
}
