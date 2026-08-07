using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap: MonoBehaviour
{
    [SerializeField] [Scene] private string _mainMenuScene;
    [SerializeField] [Scene] private string _loadingScene;

    private void Awake()
    {
        StartCoroutine(SceneLoaderService.LoadAsync(_mainMenuScene, _loadingScene));
    }
}
}