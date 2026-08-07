using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: MonoBehaviour
{
    private PlayerModel _player;
    private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    private PauseUIScreen _pauseUIScreen;

    public void Construct(EvolutionChooseUIScreen evolutionChooseUIScreen, PauseUIScreen pauseUIScreen, PlayerModel model)
    {
        _evolutionChooseUIScreen = evolutionChooseUIScreen;
        _pauseUIScreen = pauseUIScreen;
        _player = model;
        _player.Experience.OnLevelChanged += OnLevelUpdated;

        _pauseUIScreen.OnStateChanged += OnPauseScreenChanged;
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