using _Game.Scripts.Core.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Bootstrap
{
public class BootstrapScope: LifetimeScope
{
    [SerializeField] [Scene] private string _mainMenuScene;
    [SerializeField] [Scene] private string _gameplayScene;
    [SerializeField] [Scene] private string _loadingScene;

    protected override void Configure(IContainerBuilder builder)
    {
        var sceneLoader = new SceneLoaderService(
            _mainMenuScene,
            _gameplayScene,
            _loadingScene);

        builder.RegisterInstance(sceneLoader);

        builder.RegisterComponentInHierarchy<Ticker>();
        builder.RegisterComponentInHierarchy<Bootstrap>();
    }
}
}