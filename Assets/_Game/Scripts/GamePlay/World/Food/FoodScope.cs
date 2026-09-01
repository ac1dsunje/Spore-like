using _Game.Scripts.GamePlay.Entities;
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
        
        builder.RegisterEntryPoint<FoodController>();
        builder.RegisterComponent(GetComponentInChildren<FoodHealth>());
    }
}
}