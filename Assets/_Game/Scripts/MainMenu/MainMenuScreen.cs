using _Game.Scripts.Core.Services;
using _Game.Scripts.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.MainMenu
{
public class MainMenuScreen: UIScreen
{
    [SerializeField] [Scene] private string _gamePlayScene;
    [SerializeField] private Button _playButton;

    protected override void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(GoToGamePlay);
    }

    private void GoToGamePlay()
    {
        StartCoroutine(SceneLoaderService.LoadAsync(_gamePlayScene));
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }
}
}