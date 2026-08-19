using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Entity;
using _Game.Scripts.GamePlay.Module;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodScope: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(GetComponentInChildren<FoodController>());
        builder.RegisterComponent(GetComponentInChildren<ItemAnimation>());

        builder.Register<EntityStats>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<DefenseModule>(Lifetime.Scoped);
    }
}
}