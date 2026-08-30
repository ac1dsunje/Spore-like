using _Game.Scripts.GamePlay.Entities.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: MonoBehaviour
{
    [Inject] private EntityAnimation _entityAnimation;
    [Inject] private EntityStats _stats;
    [Inject] private FoodHealth _health;

    public void Initialize(FoodConfig config)
    {
        _health.SetConfig(config);

        _entityAnimation.SetConfig(config.AnimationSettings);
        
        _stats.AddInitialStats(config.StatsConfig.Stats);
    }
}
}