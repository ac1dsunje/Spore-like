using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinController: MonoBehaviour
{
    [Inject]
    private void Construct(EntityStats entityStats, EntityStatsConfig entityStatsConfig, SeaUrchinAttackBehaviour attackBehaviour,
        AnimationSettings animationSettings, ItemAnimation itemAnimation, IDamageAble damageAble)
    {
        attackBehaviour.SetOwner(damageAble);
        
        itemAnimation.SetConfig(animationSettings);
        
        entityStats.Initialize(entityStatsConfig.InitialConfigs);
    }
}
}