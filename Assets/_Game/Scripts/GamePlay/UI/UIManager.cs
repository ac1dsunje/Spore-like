using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: MonoBehaviour
{
    private PlayerModel _player;
    
    [SerializeField] private PauseUIScreen _pauseUIScreen;
    [SerializeField] private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    
    [SerializeField] private OverlayUIScreen _overlayUIScreen;
    [SerializeField] private ActiveEvolutionsDisplay _activeEvolutionsDisplay;
    [SerializeField] private ActiveAbilitiesDisplay  _activeAbilitiesDisplay;

    public void Construct(PlayerModel model)
    {
        _player = model;
        
        _player.Experience.OnLevelChanged += OnLevelUpdated;
        _pauseUIScreen.OnStateChanged += OnPauseScreenChanged;
        
        _overlayUIScreen.Construct(_player);
        _activeEvolutionsDisplay.Construct(_player.Evolutions);
        _evolutionChooseUIScreen.Construct(_player.Evolutions);
        _activeAbilitiesDisplay.Construct(_player.Abilities);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseUIScreen.ToggleScreen();
        }
    }

    private void OnPauseScreenChanged(bool state)
    {
        if (state)
            _player.Movement.Disable();
        else
            _player.Movement.Enable();
    }

    private void OnLevelUpdated(int level)
    {
        _evolutionChooseUIScreen.ShowScreen();
    }

    public void OnDestroy()
    {
        _player.Experience.OnLevelChanged -= OnLevelUpdated;

        _pauseUIScreen.OnStateChanged -= OnPauseScreenChanged;
    }
}
}