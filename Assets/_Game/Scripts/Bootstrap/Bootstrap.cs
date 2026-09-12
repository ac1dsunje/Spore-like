using _Game.Scripts.Core.Services;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap : IStartable
{
    private readonly SceneLoaderService _sceneLoaderService;

    public Bootstrap(SceneLoaderService sceneLoaderService)
    {
        _sceneLoaderService = sceneLoaderService;
    }

    public void Start()
    {
        LoadMainMenuAsync().Forget();
    }

    private async UniTaskVoid LoadMainMenuAsync()
    {
        await _sceneLoaderService.LoadMainMenu();
    }
}
}