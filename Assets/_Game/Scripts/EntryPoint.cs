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
    [SerializeField] private PlayerConfig _playerConfig;
    [Header("UI")] [SerializeField] private CameraController _camera;
    [SerializeField] private OverlayScreen _overlayScreen;
    [SerializeField] private EvolutionChooseScreen _evolutionChooseScreen;

    [Header("Evolutions")] [SerializeField]
    private EvolutionsManager _evolutionsManager;

    private void Awake()
    {
        _camera.Construct(_player.transform);

        var playerStats = new PlayerStats(_playerConfig);
        _overlayScreen.Construct(playerStats);

        _player.Construct(playerStats);
        _evolutionsManager.Construct(_player, _evolutionChooseScreen);
    }
}
}
