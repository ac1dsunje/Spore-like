using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
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
    
    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_entityConfig);
        builder.RegisterInstance(_entityExperienceConfig);
        
        // Modules
        builder.Register<EntityModel>(Lifetime.Scoped);
        builder.Register<EntityStats>(Lifetime.Scoped);
        
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
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.RegisterComponent(GetComponentInChildren<EntityVisionHitbox>());
        builder.RegisterComponent(GetComponentInChildren<EntityBodyHitbox>()).AsSelf().As<IDamageReceiver>();
        builder.RegisterComponent(GetComponentInChildren<EntityLighting>());
        builder.RegisterEntryPoint<EntityEndurance>(Lifetime.Scoped);
        
        // Important
        builder.RegisterEntryPoint<ExperienceModule>(Lifetime.Scoped).AsSelf();
    }
}
}