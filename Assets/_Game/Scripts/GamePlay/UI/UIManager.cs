using System;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: IDisposable
{
    private readonly PlayerModel _player;
    private readonly EvolutionChooseUIScreen _evolutionChooseUIScreen;

    public UIManager(EvolutionChooseUIScreen evolutionChooseUIScreen, PlayerModel model)
    {
        _evolutionChooseUIScreen = evolutionChooseUIScreen;
        _player = model;
        _player.Experience.OnLevelChanged += OnLevelUpdated;
    }

    private void OnLevelUpdated(int level)
    {
        _evolutionChooseUIScreen.Show();
    }

    public void Dispose()
    {
        _player.Experience.OnLevelChanged -= OnLevelUpdated;
    }
}
}