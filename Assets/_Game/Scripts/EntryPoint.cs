using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.UI.Choosing;
using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Endurance;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Mouth;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.Player.Modules.Vision;
using _Game.Scripts.UI;
using _Game.Scripts.World;
using UnityEngine;

namespace _Game.Scripts
{
public class EntryPoint : MonoBehaviour
{
    [Header("World")]
    [SerializeField] private WorldGenerator _worldGenerator;
    
    [Header("Player")]
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerVision _playerVision;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerMouth _playerMouth;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerEndurance _playerEndurance;
    
    [Header("UI")]
    [SerializeField] private OverlayScreen _overlayScreen;
    [SerializeField] private EvolutionChooseScreen _evolutionChooseScreen;

    [Header("Evolutions")] 
    [SerializeField] private EvolutionsManager _evolutionsManager;
    
    private PlayerModel _playerModel;

    private void Awake()
    {
        CreatePlayer();
        
        _overlayScreen.Construct(_playerModel);
        
        _worldGenerator.Construct(_player.transform);
        
        _evolutionsManager.Construct(_playerModel, _evolutionChooseScreen);
    }

    private void CreatePlayer()
    {
        _playerModel = new PlayerModel(_playerConfig);
        _playerVision.Construct(_playerModel.Vision);
        _playerMovement.Construct(_playerModel.Movement, _playerModel.Endurance);
        _playerMouth.Construct(_playerModel.EatModule);
        _playerHealth.Construct(_playerModel.Health);
        _playerEndurance.Construct(_playerModel.Endurance);
        _player.Construct(_playerModel);
    }
}
}
