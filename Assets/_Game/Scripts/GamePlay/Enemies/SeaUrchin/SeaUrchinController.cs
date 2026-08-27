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
    [Inject] private SeaUrchinAttackBehaviour _attack;
    [Inject] private SeaUrchinHealth _health;

    private void Start()
    {
        _attack.SetReceiver(_health);
        _health.SetAttackSource(_attack);
        
        _entityStats.Initialize(_entityStatsConfig.InitialConfigs);
        _itemAnimation.SetConfig(_animationSettings);
    }
}
}