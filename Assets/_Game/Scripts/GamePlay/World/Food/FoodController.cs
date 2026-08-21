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
    
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Initialize(FoodConfig config)
    {
        _health.SetConfig(config);

        _itemAnimation.SetConfig(config.AnimationSettings);
        
        _stats.AddInitialStats(config.StatsConfig.Stats);
        _collider.isTrigger = !config.IsObstacle;
    }
}
}