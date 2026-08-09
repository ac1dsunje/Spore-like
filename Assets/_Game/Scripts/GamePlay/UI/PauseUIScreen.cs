using _Game.Scripts.Core.Services;
using _Game.Scripts.Core.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class PauseUIScreen: UIScreen
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    
    [Inject] private SceneLoaderService _sceneLoaderService;

    protected override void Awake()
    {
        base.Awake();
        HideScreen();
        
        _resumeButton.onClick.AddListener(Resume);
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void GoToMainMenu()
    {
        StartCoroutine(_sceneLoaderService.LoadMainMenu());
    }

    private void Resume()
    {
        HideScreen();
    }

    private void OnDestroy()
    {
        _resumeButton.onClick.RemoveListener(Resume);
        _mainMenuButton.onClick.RemoveListener(GoToMainMenu);
    }
}
}