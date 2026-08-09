using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class GameplayLifetimeScope: LifetimeScope
{
    [SerializeField] private WorldGenerationConfig _worldConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<Ticker>();
        builder.RegisterComponentInHierarchy<PlayerSpawner>();
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        builder.RegisterComponentInHierarchy<UIManager>();
        builder.RegisterComponentInHierarchy<EntryPoint>();

        builder.RegisterInstance(_worldConfig);

        builder.Register<WorldModel>(Lifetime.Singleton);
    }
}
}