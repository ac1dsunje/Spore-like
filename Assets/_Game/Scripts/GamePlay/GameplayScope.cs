using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.Rarities;
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
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    
    [SerializeField] private BuffsDatabase _buffsDatabase;

    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_statTypeConfig);
        builder.RegisterInstance(_evolutionsDatabase);
        builder.RegisterInstance(_raritiesDatabase);
        builder.RegisterInstance(_worldConfig);
        builder.RegisterInstance(_buffsDatabase);
        
        // World
        builder.RegisterEntryPoint<WorldGenerator>().AsSelf();
        builder.RegisterComponentInHierarchy<WorldTileRenderer>();
        builder.RegisterComponentInHierarchy<EnvironmentSpawner>();

        builder.RegisterComponentInHierarchy<DayNightManager>();
        builder.Register<WorldModel>(Lifetime.Singleton);
        
        // UI
        builder.Register<EvolutionFormatter>(Lifetime.Transient);
        
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
        builder.RegisterEntryPoint<CameraController>().AsSelf();
        builder.RegisterComponentInHierarchy<Camera>();
        builder.RegisterComponentInHierarchy<CinemachineCamera>();
        
        // Factories
        builder.Register<AbilityFactory>(Lifetime.Singleton);
        builder.Register<ExperienceFactory>(Lifetime.Singleton);
        
        // Entities
        builder.RegisterComponentInHierarchy<EntitySpawner>();
        builder.RegisterComponentInHierarchy<EntitiesRegistry>();
        builder.RegisterComponentInHierarchy<DropSpawner>();
        builder.RegisterComponentInHierarchy<ParticlesSpawner>();
    }
}
}