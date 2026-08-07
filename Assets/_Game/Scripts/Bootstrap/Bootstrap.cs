using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap: MonoBehaviour
{
    [SerializeField] private CoroutinePerformer _coroutinePerformer;
    [SerializeField] [Scene] private string _mainMenuScene;
    [SerializeField] [Scene] private string _gamePlayScene;
    [SerializeField] [Scene] private string _loadingScene;

    private void Awake()
    {
        _coroutinePerformer.StartCoroutine(SceneLoaderService.LoadAsync(_gamePlayScene, _loadingScene));
    }
}
}