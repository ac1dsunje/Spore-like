using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Core.Services
{
public static class SceneLoaderService
{
    private static string LoadingScene;

    public static IEnumerator LoadAsync(string sceneName, string loadingScene = "LoadingScene")
    {
        LoadingScene = loadingScene;
        
        var operation = SceneManager.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => operation.isDone);
        
        var waitLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => waitLoading.isDone);
        SceneManager.UnloadSceneAsync(LoadingScene);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
    
}
}