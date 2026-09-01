using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodScope: EntityScope
{
    private FoodConfig _config;
    
    public void SetConfig(FoodConfig config) => _config = config;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterInstance(_config);
        builder.RegisterInstance(_config.AnimationSettings);
        builder.RegisterInstance(_config.EntityStatsConfig);
        
        builder.RegisterEntryPoint<FoodController>();
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.RegisterComponent(GetComponentInChildren<FoodHealth>());

        builder.Register<EntityStats>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<DefenseModule>(Lifetime.Scoped);
        builder.Register<DisguiseModule>(Lifetime.Scoped);
    }
}
}