using _Game.Scripts.GamePlay.Entities.Animation;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: IStartable
{
    [Inject] private EntityAnimation _entityAnimation;
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private StatsConfig _statsConfig;
    [Inject] private EntityStats _stats;

    public void Start()
    {
        _entityAnimation.SetConfig(_animationSettings);
        
        _stats.AddInitialStats(_statsConfig.Stats);
    }
}
}