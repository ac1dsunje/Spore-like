using _Game.Scripts.GamePlay.Animation;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: IStartable
{
    [Inject] private EntityStats _entityStats;
    [Inject] private EntityStatsConfig _entityStatsConfig;
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private ItemAnimation _itemAnimation;

    public void Start()
    {
        _entityStats.Initialize(_entityStatsConfig.InitialConfigs);
        _itemAnimation.SetConfig(_animationSettings);
    }
}
}