using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class GameplayScope: LifetimeScope
{
    [SerializeField] private WorldGenerationConfig _worldConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerSpawner>();
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        builder.RegisterComponentInHierarchy<UIManager>();
        builder.RegisterComponentInHierarchy<EntryPoint>();

        builder.RegisterComponentInHierarchy<PauseUIScreen>();

        builder.RegisterInstance(_worldConfig);

        builder.Register<WorldModel>(Lifetime.Scoped);
        builder.Register<PlayerRegistry>(Lifetime.Scoped);
        builder.Register<PlayerInputService>(Lifetime.Scoped);
    }
}
}