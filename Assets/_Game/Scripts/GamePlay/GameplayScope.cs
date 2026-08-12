using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
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
    [Header("Config")]
    [SerializeField] private WorldGenerationConfig _worldConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        // Player
        builder.RegisterComponentInHierarchy<PlayerSpawner>();
        builder.Register<PlayerRegistry>(Lifetime.Scoped);
        builder.Register<PlayerInputService>(Lifetime.Scoped);
        
        // World
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        builder.RegisterComponentInHierarchy<WorldTileRenderer>();
        builder.RegisterComponentInHierarchy<EnvironmentSpawner>();
        builder.RegisterInstance(_worldConfig);
        builder.Register<WorldModel>(Lifetime.Scoped);
        
        // UI
        builder.RegisterComponentInHierarchy<UIManager>();
        builder.RegisterComponentInHierarchy<EvolutionChooseUIScreen>();
        builder.RegisterComponentInHierarchy<OverlayUIScreen>();
        builder.RegisterComponentInHierarchy<ActiveEvolutionsDisplay>();
        builder.RegisterComponentInHierarchy<ActiveAbilitiesDisplay>();
        builder.RegisterComponentInHierarchy<PauseUIScreen>();
        builder.RegisterComponentInHierarchy<CameraController>();
    }
}
}