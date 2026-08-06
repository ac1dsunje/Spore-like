using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Ticker _ticker;
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
    private UIManager _uiManager;

    private void Awake()
    {
        CreatePlayer();
        
        _overlayScreen.Construct(_playerModel);
        
        _worldGenerator.Construct(_player.transform);
        
        _evolutionsManager.Construct(_playerModel);
        
        _evolutionChooseScreen.Construct(_evolutionsManager);
        _uiManager = new(_evolutionChooseScreen, _playerModel);
    }

    private void CreatePlayer()
    {
        _playerModel = new PlayerModel(_playerConfig);
        _playerVision.Construct(_playerModel.Vision);
        _playerMovement.Construct(_playerModel.Movement);
        _playerMouth.Construct(_playerModel.EatModule);
        _playerHealth.Construct(_playerModel.Health);
        _playerEndurance.Construct(_playerModel.Endurance);

        var abilityFactory = new AbilityFactory(_playerModel, _ticker);
        _playerModel.Abilities.SetFactory(abilityFactory);
        
        _player.Construct(_playerModel);
    }

    private void OnDestroy()
    {
        _uiManager.Dispose();
    }
}
}
