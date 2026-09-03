using _Game.Scripts.GamePlay.Enemies.SeaUrchin;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.World.Biomes.Plant;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
[RequireComponent(typeof(CoroutineRunner))]
[RequireComponent(typeof(RigidbodyController))]
public class EntityScope: LifetimeScope
{
    private AnimationSettings _animationSettings;
    private StatsConfig _entityStatsConfig;
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
        
        builder.RegisterEntryPoint<VisionModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<DisguiseModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<HealthModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<AttackModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<DefenseModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<EnduranceModule>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterEntryPoint<MouthModule>(Lifetime.Scoped).AsSelf();
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
        
        // Coroutines
        builder.RegisterComponent(GetComponentInChildren<CoroutineRunner>());

        ChooseBehaviour(_entityConfig.EntityType, builder);
    }

    private void ChooseBehaviour(EntityType entityType, IContainerBuilder builder)
    {
        switch (entityType)
        {
            case EntityType.Food:
                builder.RegisterEntryPoint<PlantAI>(Lifetime.Scoped);
                builder.RegisterEntryPoint<PlantHealth>()
                    .As<IHealthController>();
                break;
            
            case EntityType.Player:
                builder.RegisterEntryPoint<PlayerAI>(Lifetime.Scoped);
                builder.RegisterEntryPoint<PlayerHealth>()
                    .As<IHealthController>();
                builder.RegisterComponent(GetComponentInChildren<PlayerAttack>())
                    .As<IDamageSource>()
                    .As<IAttackController>();
                builder.RegisterEntryPoint<PlayerVision>(Lifetime.Scoped);
                break;
            
            case EntityType.SeaUrchin:
                builder.RegisterEntryPoint<SeaUrchinAI>(Lifetime.Scoped);
                builder.RegisterEntryPoint<SeaUrchinHealth>()
                    .As<IHealthController>();
                builder.RegisterEntryPoint<EntityBasicAttackBehaviour>()
                    .As<IDamageSource>()
                    .As<IAttackController>();
                break;
        }
    }
}
}