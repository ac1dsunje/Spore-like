using _Game.Scripts.GamePlay.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: MonoBehaviour
{
    [Inject] private EntityStats _entityStats;
    [Inject] private EntityStatsConfig _entityStatsConfig;
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private ItemAnimation _itemAnimation;

    private void Start()
    {
        _entityStats.Initialize(_entityStatsConfig.InitialConfigs);
        _itemAnimation.SetConfig(_animationSettings);
    }
}
}