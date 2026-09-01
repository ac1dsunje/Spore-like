using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: IStartable
{
    [Inject] private EntityStats _stats;

    public void Start()
    {
        _stats.Initialize();
    }
}
}