using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Core.Services
{
public class SceneLoader: MonoBehaviour
{
    [SerializeField] [Scene] private string _mainMenuScene;
    [SerializeField] [Scene] private string _gamePlayScene;
    [SerializeField] [Scene] private string _loadingScene;

    public void LoadMainMenuScene()
    {
        LoadScene(_mainMenuScene);
    }

    public void LoadGamePlayScene()
    {
        LoadScene(_gamePlayScene);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(_loadingScene, LoadSceneMode.Additive);

        StartCoroutine(LoadTargetScene(sceneName));
    }

    private IEnumerator LoadTargetScene(string targetSceneName)
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);
        yield return new WaitForSeconds(.2f);
        SceneManager.UnloadSceneAsync(_loadingScene);
    }
    
}
}