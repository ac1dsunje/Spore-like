using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: MonoBehaviour
{
    [Inject] private EntityStats _entityStats;
    [Inject] private EntityStatsConfig _entityStatsConfig;
    [Inject] private SeaUrchinAttackBehaviour _attackBehaviour;
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private ItemAnimation _itemAnimation;
    [Inject] private IDamageAble _damageAble;

    private void Start()
    {
        _attackBehaviour.SetOwner(_damageAble);
        
        _entityStats.Initialize(_entityStatsConfig.InitialConfigs);
        _itemAnimation.SetConfig(_animationSettings);
    }
}
}