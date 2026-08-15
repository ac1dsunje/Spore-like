using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Network;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerVision: EntityNetworkBehaviour
{
    [SerializeField] private BoxCollider2D _visionCollider;
    [SerializeField] private Light2D _visionLight;
        
    private VisionModule _module;
    private CinemachineCamera _cineMachine;
    private Camera _camera;
        
    [Inject]
    private void Construct(VisionModule module, CinemachineCamera cineMachine, Camera cam)
    {
        _module = module;
        _cineMachine = cineMachine;
        _camera = cam;
    }

    protected override void OnNetworkInitialized()
    {
        _visionCollider.enabled = IsLocal;
        if (IsLocal)
        {
            _module.OnVisionRadiusUpdated += UpdateVision;
            _module.OnLightingUpdated += UpdateLighting;
            
            UpdateVision(_module.VisionRadius);
        }
    }

    private void UpdateLighting(float value, bool state)
    {
        _visionLight.pointLightOuterRadius = state? value : 0f;
    }

    private void UpdateVision(float value)
    {
        _visionCollider.size = new Vector2(value * _camera.aspect, value) * 2;
        _cineMachine.Lens.OrthographicSize = value;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        _module.DiscoverGameObject(other.gameObject);
    }
    
    protected override void OnDestroy()
    {
        _module.OnLightingUpdated -= UpdateLighting;
        _module.OnVisionRadiusUpdated -= UpdateVision;
        base.OnDestroy();
    }
}
}