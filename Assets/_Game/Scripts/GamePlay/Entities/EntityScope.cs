using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public abstract class EntityScope: LifetimeScope
{
    private AnimationSettings _animationSettings;
    private EntityStatsConfig _entityStatsConfig;
    
    public void SetAnimationSetting(AnimationSettings settings) => _animationSettings = settings;
    public void SetStatsSettings(EntityStatsConfig config) => _entityStatsConfig = config;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // Configs
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_entityStatsConfig);
        
        // Modules
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
        builder.RegisterEntryPoint<EntityEndurance>(Lifetime.Scoped);
    }
}
}