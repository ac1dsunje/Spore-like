using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityScope : LifetimeScope
{
    private readonly EntityBuilder _entityBuilder = new();
    
    private EntityConfig _entityConfig;

    public void Initialize(EntityConfig entityConfig)
    {
        _entityConfig = entityConfig;
        Build();
    }

    public T Get<T>() => Container.Resolve<T>();

    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(transform);
        builder.RegisterInstance(_entityConfig);
        builder.RegisterInstance(_entityConfig.AnimationSettings);
        builder.RegisterInstance(_entityConfig.ExperienceConfig);
        builder.RegisterInstance(_entityConfig.Drops);
        builder.RegisterInstance(_entityConfig.Projectile);
        
        // Modules
        builder.RegisterEntryPoint<EntityStats>(Lifetime.Scoped).AsSelf();
        
        builder.Register<VisionModule>(Lifetime.Scoped);
        
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<RegenerationModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        builder.Register<DefenseModule>(Lifetime.Scoped);
        
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        builder.Register<PickingModule>(Lifetime.Scoped);
        builder.Register<StomachModule>(Lifetime.Scoped);
        
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        builder.Register<EnvironmentModule>(Lifetime.Scoped);
        builder.Register<BreathingModule>(Lifetime.Scoped);
        builder.Register<SocialModule>(Lifetime.Scoped);
        
        // Behaviours
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.RegisterComponent(GetComponentInChildren<VisionHitbox>());
        builder.RegisterComponent(GetComponentInChildren<BodyHitbox>())
            .AsSelf()
            .As<IDamageReceiver>();
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
        builder.RegisterEntryPoint<EntityStomach>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityPicker>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityHealth>(Lifetime.Scoped).As<IHealthController>();
        builder.RegisterEntryPoint<EntityDeath>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityRegeneration>(Lifetime.Scoped);
        builder.RegisterEntryPoint<EntityWeaponAttack>(Lifetime.Scoped).As<IDamageSource>().As<IAttackController>();
        
        _entityBuilder.ChooseBehaviour(_entityConfig.AIType, builder);
    }
}
}