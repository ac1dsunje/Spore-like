using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Rarities;
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

    [Header("UI")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private OverlayUIScreen _overlayUIScreen;
    [SerializeField] private PauseUIScreen _pauseUIScreen;
    [SerializeField] private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;

    private EvolutionsManager _evolutionsManager;

    private void Awake()
    {
        _player.Initialize(_ticker);
        
        _overlayUIScreen.Construct(_player.Model);
        
        _worldGenerator.Construct(_player.transform);
        
        _evolutionsManager = new(_player.Model, _evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        
        _evolutionChooseUIScreen.Construct(_evolutionsManager);
        _uiManager.Construct(_evolutionChooseUIScreen, _pauseUIScreen, _player.Model);
    }

    private void OnDestroy()
    {
        _evolutionsManager.Dispose();
    }
}
}
