using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI.Choosing;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules;
using _Game.Scripts.Player.Modules.Vision;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerVision _playerVision;
    [SerializeField] private PlayerMovement _playerMovement;
    [Header("UI")]
    [SerializeField] private OverlayScreen _overlayScreen;
    [SerializeField] private EvolutionChooseScreen _evolutionChooseScreen;

    [Header("Evolutions")] [SerializeField]
    private EvolutionsManager _evolutionsManager;

    private void Awake()
    {
        var playerStats = new PlayerStats(_playerConfig);
        _overlayScreen.Construct(playerStats);

        _playerVision.Construct(playerStats.Vision);
        _playerMovement.Construct(playerStats);
        _player.Construct(playerStats, _playerMovement);
        
        _evolutionsManager.Construct(_player, _evolutionChooseScreen);
    }
}
}
