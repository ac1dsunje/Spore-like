using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: IStartable
{
    [Inject] private EntityStats _entityStats;
    [Inject] private EntityStatsConfig _entityStatsConfig;

    public void Start()
    {
        _entityStats.Initialize(_entityStatsConfig.InitialConfigs);
    }
}
}