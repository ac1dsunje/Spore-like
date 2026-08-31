using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
[RequireComponent(typeof(Light2D))]
public class EntityLighting: MonoBehaviour
{
    private Light2D _lighting;
    
    private VisionModule _module;

    private void Awake()
    {
        _lighting = GetComponent<Light2D>();
    }

    [Inject]
    private void Construct(VisionModule module)
    {
        _module = module;
        
        _module.OnLightingUpdated += UpdateLighting;
    }

    private void UpdateLighting(float value, bool state) => _lighting.pointLightOuterRadius = state ? value : 0f;

    private void OnDestroy()
    {
        _module.OnLightingUpdated -= UpdateLighting;
    }
}
}