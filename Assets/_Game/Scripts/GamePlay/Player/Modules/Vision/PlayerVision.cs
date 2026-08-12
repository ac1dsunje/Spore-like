using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Vision
{
public class PlayerVision: MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private CircleCollider2D _visionCollider;
        
    private VisionModule _module;
        
    [Inject]
    private void Construct(VisionModule module)
    {
        _module = module;
            
        _module.OnVisionRadiusChanged += UpdateVisuals;
            
        UpdateVisuals(_module.VisionRadius);
    }

    private void UpdateVisuals(float radius)
    {
        _light.pointLightOuterRadius = radius;
        _visionCollider.radius = radius;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) return;
        _module.DiscoverGameObject(other.gameObject);
    }

    private void OnDestroy()
    {
        if (_module != null)
        {
            _module.OnVisionRadiusChanged -= UpdateVisuals;
        }
    }
}
}