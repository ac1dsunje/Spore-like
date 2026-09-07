using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.Entities.Death;
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
[RequireComponent(typeof(RigidbodyController))]
public class EntityScope: LifetimeScope
{
    private readonly EntityBuilder _entityBuilder = new();
    
    private EntityConfig _entityConfig;

    public void SetConfig(EntityConfig entityConfig)
    {
        _entityConfig = entityConfig;
    }

    public EntityController GetEntityController() => Container.Resolve<EntityController>();

    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_entityConfig);
        builder.RegisterInstance(_entityConfig.AnimationSettings);
        builder.RegisterInstance(_entityConfig.EntityStatsConfig);
        builder.RegisterInstance(_entityConfig.ExperienceConfig);
        builder.RegisterInstance(_entityConfig.Drops);
        builder.RegisterInstance(_entityConfig.Projectile);
        
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
        
        builder.RegisterEntryPoint<EnvironmentModule>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<SocialModule>(Lifetime.Scoped).AsSelf();
        
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
        builder.RegisterComponent(GetComponentInChildren<PickerHitbox>());
        builder.RegisterEntryPoint<EntityBasicMovement>(Lifetime.Scoped)
            .As<IMovementController>();
        builder.RegisterEntryPoint<EntityVision>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntitySocial>(Lifetime.Scoped);
        
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
        builder.RegisterEntryPoint<EntityBasicHealth>().As<IHealthController>();
        builder.RegisterEntryPoint<EntityBasicDeath>();
        builder.RegisterEntryPoint<EntityRegeneration>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityWeaponAttack>().As<IDamageSource>().As<IAttackController>();
        
        _entityBuilder.ChooseBehaviour(_entityConfig.AIType, builder);
    }
}
}