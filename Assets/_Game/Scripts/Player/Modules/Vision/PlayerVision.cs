using _Game.Scripts.World.Food;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.Player.Modules.Vision
{
public class PlayerVision: MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private CircleCollider2D _visionCollider;
        
    private VisionStats _stats;
        
    public void Construct(VisionStats stats)
    {
        _stats = stats;
            
        _stats.OnVisionRadiusChanged += UpdateVisuals;
            
        UpdateVisuals(_stats.VisionRadius);
    }

    private void UpdateVisuals(float radius)
    {
        _light.pointLightOuterRadius = radius;
        _visionCollider.radius = radius;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<FoodItem>(out var food))
        {
            _stats.DiscoverFood(food);
        }
    }

    private void OnDestroy()
    {
        if (_stats != null)
        {
            _stats.OnVisionRadiusChanged -= UpdateVisuals;
        }
    }
}
}