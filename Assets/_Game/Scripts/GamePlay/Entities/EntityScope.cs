using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
[RequireComponent(typeof(CoroutineRunner))]
public abstract class EntityScope: LifetimeScope
{
    private AnimationSettings _animationSettings;
    private EntityStatsConfig _entityStatsConfig;
    private EntityConfig _entityConfig;
    private EntityExperienceConfig _entityExperienceConfig;

    public void SetConfig(EntityConfig entityConfig)
    {
        _entityConfig = entityConfig;
        _animationSettings = entityConfig.AnimationSettings;
        _entityStatsConfig = entityConfig.EntityStatsConfig;
        _entityExperienceConfig = entityConfig.ExperienceConfig;
    }

    public EntityController GetEntityController() => Container.Resolve<EntityController>();

    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_entityConfig);
        builder.RegisterInstance(_entityExperienceConfig);
        
        // Modules
        builder.Register<EntityModel>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityStats>(Lifetime.Scoped).AsSelf();
        
        builder.Register<VisionModule>(Lifetime.Scoped);
        builder.Register<DisguiseModule>(Lifetime.Scoped);
        
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        builder.Register<DefenseModule>(Lifetime.Scoped);
        
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        builder.Register<MouthModule>(Lifetime.Scoped);
        builder.Register<StomachModule>(Lifetime.Scoped);
        
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        builder.Register<BiomeModule>(Lifetime.Scoped);
        builder.Register<BreathingModule>(Lifetime.Scoped);
        builder.Register<TemperatureModule>(Lifetime.Scoped);
        
        // Behaviours
        builder.RegisterEntryPoint<EntityController>(Lifetime.Scoped).AsSelf();
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.RegisterComponent(GetComponentInChildren<VisionHitbox>());
        builder.RegisterComponent(GetComponentInChildren<BodyHitbox>()).AsSelf().As<IDamageReceiver>().As<IBiteable>();
        builder.RegisterComponent(GetComponentInChildren<EntityLighting>());
        builder.RegisterEntryPoint<EntityEndurance>(Lifetime.Scoped);
        
        // Important
        builder.RegisterEntryPoint<ExperienceModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<BuffsModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<BiomeChecker>(Lifetime.Scoped);
        builder.RegisterEntryPoint<AbilitiesModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<EvolutionsModule>(Lifetime.Scoped).AsSelf();
        
        // Coroutines
        builder.RegisterComponent(GetComponentInChildren<CoroutineRunner>());
    }
}
}