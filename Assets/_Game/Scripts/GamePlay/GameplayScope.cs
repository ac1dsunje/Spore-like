using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Evolutions.UI;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Types;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class GameplayScope: LifetimeScope
{
    [Header("Configs")]
    [SerializeField] private WorldGenerationConfig _worldConfig;
    [SerializeField] private StatTypeConfig _statTypeConfig;
    
    [SerializeField] private BuffsDatabase _buffsDatabase;

    protected override void Configure(IContainerBuilder builder)
    {
        // Stats
        builder.RegisterInstance(_statTypeConfig);
        
        // Player
        builder.Register<PlayerRegistry>(Lifetime.Singleton);
        
        // World
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        builder.RegisterComponentInHierarchy<WorldTileRenderer>();
        builder.RegisterComponentInHierarchy<EnvironmentSpawner>();

        builder.RegisterComponentInHierarchy<DayNightManager>();
        
        builder.RegisterInstance(_worldConfig);
        builder.Register<WorldModel>(Lifetime.Singleton);
        
        // UI
        builder.Register<EvolutionFormatter>(Lifetime.Singleton);
        
        builder.RegisterComponentInHierarchy<UIManager>();
        builder.RegisterComponentInHierarchy<EvolutionChooseUIScreen>();
        builder.RegisterComponentInHierarchy<OverlayUIScreen>();
        builder.RegisterComponentInHierarchy<ActiveEvolutionsDisplay>();
        builder.RegisterComponentInHierarchy<ActiveAbilitiesDisplay>();
        builder.RegisterComponentInHierarchy<ActiveBuffsDisplay>();
        builder.RegisterComponentInHierarchy<PauseUIScreen>();
        builder.RegisterComponentInHierarchy<BarsPanelUI>();
        builder.RegisterComponentInHierarchy<DescriptionUI>();
        
        // Cameras
        builder.RegisterComponentInHierarchy<CameraController>();
        builder.RegisterComponentInHierarchy<Camera>();
        builder.RegisterComponentInHierarchy<CinemachineCamera>();
        
        // Abilities
        builder.Register<AbilityFactory>(Lifetime.Scoped);
        
        // Buffs
        builder.RegisterInstance(_buffsDatabase);
    }
}
}