using _Game.Scripts.Core.Services;
using VContainer.Unity;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap : IStartable
{
    private readonly SceneLoaderService _sceneLoaderService;
    private readonly CoroutineRunner _coroutineRunner;

    public Bootstrap(SceneLoaderService sceneLoaderService, CoroutineRunner coroutineRunner)
    {
        _sceneLoaderService = sceneLoaderService;
        _coroutineRunner = coroutineRunner;
    }

    public void Start()
    {
        _coroutineRunner.Run(this, "LoadMainMenu", _sceneLoaderService.LoadMainMenu());
    }
}
}