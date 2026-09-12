using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Core.Services
{
public class SceneLoaderService
{
    private readonly string _mainMenuScene;
    private readonly string _gameplayScene;
    private readonly string _loadingScene;

    private string _currentScene;

    public SceneLoaderService(string mainMenuScene, string gameplayScene, string loadingScene)
    {
        _mainMenuScene = mainMenuScene;
        _gameplayScene = gameplayScene;
        _loadingScene = loadingScene;
    }

    public async UniTask LoadMainMenu(CancellationToken cancellationToken = default)
    {
        await LoadScene(_mainMenuScene, cancellationToken);
    }

    public async UniTask LoadGameplay(CancellationToken cancellationToken = default)
    {
        await LoadScene(_gameplayScene, cancellationToken);
    }

    private async UniTask LoadScene(string sceneName, CancellationToken cancellationToken)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(_loadingScene, LoadSceneMode.Additive);
        await loadingOperation.ToUniTask(cancellationToken: cancellationToken);

        var sceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        await sceneOperation.ToUniTask(cancellationToken: cancellationToken);

        if (!string.IsNullOrEmpty(_currentScene))
        {
            var unloadPreviousOperation = SceneManager.UnloadSceneAsync(_currentScene);
            if (unloadPreviousOperation != null)
            {
                await unloadPreviousOperation.ToUniTask(cancellationToken: cancellationToken);
            }
        }

        _currentScene = sceneName;
        
        var unloadLoadingOperation = SceneManager.UnloadSceneAsync(_loadingScene);
        if (unloadLoadingOperation != null)
        {
            await unloadLoadingOperation.ToUniTask(cancellationToken: cancellationToken);
        }
    }
}
}