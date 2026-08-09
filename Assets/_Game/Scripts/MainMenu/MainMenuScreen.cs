using _Game.Scripts.Core.Services;
using _Game.Scripts.Core.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.MainMenu
{
public class MainMenuScreen: UIScreen
{
    [SerializeField] private Button _playButton;

    [Inject] private SceneLoaderService _sceneLoaderService;

    protected override void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(GoToGamePlay);
    }

    private void GoToGamePlay()
    {
        StartCoroutine(_sceneLoaderService.LoadGameplay());
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }
}
}