using _Game.Scripts.GamePlay.Entities;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodScope: EntityScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        
        builder.RegisterEntryPoint<EntityController>(Lifetime.Scoped).AsSelf();
        builder.RegisterComponent(GetComponentInChildren<FoodHealth>());
    }
}
}