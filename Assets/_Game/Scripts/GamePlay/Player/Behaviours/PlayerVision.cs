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
    [SerializeField] private float _visionChangeSpeed = 5f;
    
    private VisionModule _module;
    private CinemachineCamera _cineMachine;
    private Camera _camera;
    
    private float _targetVision;
    private float _currentVision;

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
            
            _currentVision = _module.VisionRadius;
            _targetVision = _module.VisionRadius;
            
            ApplyVision(_currentVision);
        }
    }

    private void Update()
    {
        if (!IsLocal) return;

        if (Mathf.Approximately(_currentVision, _targetVision)) return;

        _currentVision = Mathf.MoveTowards(_currentVision, _targetVision, _visionChangeSpeed * Time.deltaTime);

        ApplyVision(_currentVision);
    }

    private void UpdateLighting(float value, bool state)
    {
        _visionLight.pointLightOuterRadius = state ? value : 0f;
    }

    private void UpdateVision(float value)
    {
        _targetVision = value;
    }

    private void ApplyVision(float value)
    {
        _visionCollider.size = new Vector2(value * _camera.aspect, value) * 2f;

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