using _Game.Scripts.World.Food;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.Player.Modules
{
public class PlayerVision: MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private CircleCollider2D _visionCollider;
    
    private PlayerStats _stats;
    
    public void Construct(PlayerStats playerStats)
    {
        _stats = playerStats;   
    }
    
    private void Update()
    {
        _light.pointLightOuterRadius = _stats.VisionRadius;
        _visionCollider.radius = _stats.VisionRadius;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<FoodItem>(out var food);
        if (food == null) return;
        _stats.DiscoverFood(food);
    }
}
}