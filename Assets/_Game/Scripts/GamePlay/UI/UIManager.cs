using System;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: IDisposable
{
    private readonly PlayerModel _player;
    private readonly EvolutionChooseScreen _evolutionChooseScreen;

    public UIManager(EvolutionChooseScreen evolutionChooseScreen, PlayerModel model)
    {
        _evolutionChooseScreen = evolutionChooseScreen;
        _player = model;
        _player.Experience.OnLevelChanged += OnLevelUpdated;
    }

    private void OnLevelUpdated(int level)
    {
        _evolutionChooseScreen.Show();
    }

    public void Dispose()
    {
        _player.Experience.OnLevelChanged -= OnLevelUpdated;
    }
}
}