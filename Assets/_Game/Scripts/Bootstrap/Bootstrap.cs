using _Game.Scripts.Core.Services;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap: MonoBehaviour
{
    [Inject] private SceneLoaderService _sceneLoaderService;

    private void Start()
    {
        StartCoroutine(_sceneLoaderService.LoadMainMenu());
    }
}
}