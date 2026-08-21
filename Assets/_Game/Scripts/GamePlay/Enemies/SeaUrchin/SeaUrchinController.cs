using _Game.Scripts.GamePlay.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: MonoBehaviour
{
    private EntityStats _entityStats;

    [Inject]
    private void Construct(EntityStats entityStats, EntityStatsConfig entityStatsConfig, SeaUrchinAttackBehaviour attackBehaviour,
        AnimationSettings animationSettings, ItemAnimation itemAnimation, SeaUrchinHealth health)
    {
        attackBehaviour.SetOwner(health);
        _entityStats = entityStats;
        
        itemAnimation.SetConfig(animationSettings);
        
        _entityStats.Initialize(entityStatsConfig.InitialConfigs);
    }
}
}