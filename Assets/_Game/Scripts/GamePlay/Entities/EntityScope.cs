using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.Entities.Drops;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Entities.Picker;
using _Game.Scripts.GamePlay.Entities.Stomach;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
[RequireComponent(typeof(CoroutineRunner))]
[RequireComponent(typeof(RigidbodyController))]
public class EntityScope: LifetimeScope
{
    private readonly EntityBuilder _entityBuilder = new();
    
    private AnimationSettings _animationSettings;
    private StatsConfig _entityStatsConfig;
    private EntityConfig _entityConfig;
    private EntityExperienceConfig _entityExperienceConfig;
    private DropsConfig _dropConfig;

    public void SetConfig(EntityConfig entityConfig)
    {
        _entityConfig = entityConfig;
        _animationSettings = entityConfig.AnimationSettings;
        _entityStatsConfig = entityConfig.EntityStatsConfig;
        _entityExperienceConfig = entityConfig.ExperienceConfig;
        _dropConfig = entityConfig.Drops;
    }

    public EntityController GetEntityController() => Container.Resolve<EntityController>();

    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_entityConfig);
        builder.RegisterInstance(_entityExperienceConfig);
        builder.RegisterInstance(_dropConfig);
        
        // Modules
        builder.Register<EntityModel>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityStats>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<VisionModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<DisguiseModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<HealthModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<AttackModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<DefenseModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<EnduranceModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<PickingModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<StomachModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<MovementModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<BiomeModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<BreathingModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<TemperatureModule>(Lifetime.Scoped).AsSelf();
        
        // Behaviours
        builder.RegisterEntryPoint<EntityController>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.RegisterComponent(GetComponentInChildren<VisionHitbox>());
        builder.RegisterComponent(GetComponentInChildren<BodyHitbox>())
            .AsSelf()
            .As<IDamageReceiver>();
        builder.RegisterComponent(GetComponentInChildren<EntityLighting>());
        builder.RegisterEntryPoint<EntityEndurance>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponentInChildren<RigidbodyController>());
        builder.RegisterComponent(GetComponentInChildren<EntityDropper>());
        builder.RegisterComponent(GetComponentInChildren<PickerHitbox>());
        builder.RegisterEntryPoint<EntityBasicMovement>(Lifetime.Scoped)
            .As<IMovementController>();
        
        // Important
        builder.RegisterEntryPoint<ExperienceModule>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterEntryPoint<BuffsModule>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterEntryPoint<BiomeChecker>(Lifetime.Scoped);
        builder.RegisterEntryPoint<AbilitiesModule>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterEntryPoint<EvolutionsModule>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterEntryPoint<ParticlesModule>(Lifetime.Scoped)
            .AsSelf();
        builder.RegisterComponent(GetComponentInChildren<EntityWeaponHolder>());
        builder.RegisterEntryPoint<EntityBasicStomach>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityPicker>(Lifetime.Scoped);
        
        // Coroutines
        builder.RegisterComponent(GetComponentInChildren<CoroutineRunner>());
        builder.RegisterEntryPoint<EntityRegeneration>(Lifetime.Scoped);

        _entityBuilder.ChooseBehaviour(_entityConfig.Data, builder, _entityConfig.Projectile);
    }
}
}