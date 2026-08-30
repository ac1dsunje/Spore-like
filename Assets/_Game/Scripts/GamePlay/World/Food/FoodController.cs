using _Game.Scripts.GamePlay.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: MonoBehaviour
{
    [Inject] private ItemAnimation _itemAnimation;
    [Inject] private EntityStats _stats;
    [Inject] private FoodHealth _health;

    public void Initialize(FoodConfig config)
    {
        _health.SetConfig(config);

        _itemAnimation.SetConfig(config.AnimationSettings);
        
        _stats.AddInitialStats(config.StatsConfig.Stats);
    }
}
}