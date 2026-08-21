using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Modules;
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
        builder.RegisterComponent(GetComponentInChildren<FoodHealth>());

        builder.Register<EntityStats>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<DefenseModule>(Lifetime.Scoped);
    }
}
}