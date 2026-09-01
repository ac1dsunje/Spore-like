using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: IStartable
{
    [Inject] private StatsConfig _statsConfig;
    [Inject] private EntityStats _stats;

    public void Start()
    {
        _stats.AddInitialStats(_statsConfig.Stats);
    }
}
}