using System.Collections;
using UnityEngine;
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

    public IEnumerator LoadMainMenu()
    {
        yield return LoadScene(_mainMenuScene);
    }

    public IEnumerator LoadGameplay()
    {
        yield return LoadScene(_gameplayScene);
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var loading = SceneManager.LoadSceneAsync(_loadingScene, LoadSceneMode.Additive);

        yield return new WaitUntil(() => loading.isDone);


        var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        yield return new WaitUntil(() => scene.isDone);


        if (!string.IsNullOrEmpty(_currentScene))
        {
            SceneManager.UnloadSceneAsync(_currentScene);
        }

        _currentScene = sceneName;
        
        SceneManager.UnloadSceneAsync(_loadingScene);
    }
}
}