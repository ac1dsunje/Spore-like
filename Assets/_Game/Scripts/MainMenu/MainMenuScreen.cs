using _Game.Scripts.Core.Services;
using _Game.Scripts.Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.MainMenu
{
public class MainMenuScreen : UIScreen
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
        GoToGamePlayAsync().Forget();
    }

    private async UniTaskVoid GoToGamePlayAsync()
    {
        await _sceneLoaderService.LoadGameplay();
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }
}
}