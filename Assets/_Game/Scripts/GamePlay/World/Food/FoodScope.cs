using _Game.Scripts.GamePlay.Animation;
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
    }
}
}