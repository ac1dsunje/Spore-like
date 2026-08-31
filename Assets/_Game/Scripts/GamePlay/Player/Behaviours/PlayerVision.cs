using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerVision: MonoBehaviour
{
    [SerializeField] private BoxCollider2D _visionCollider;
    [SerializeField] private Light2D _visionLight;
    [SerializeField] private Light2D _lighting;
    [SerializeField] private float _visionChangeSpeed = 5f;
    
    private VisionModule _module;
    private CinemachineCamera _cineMachine;
    private Camera _camera;
    private DayNightManager _dayNightManager;
    
    private float _targetVision;
    private float _currentVision;

    [Inject]
    private void Construct(VisionModule module, CinemachineCamera cineMachine, Camera cam, DayNightManager dayNightManager)
    {
        _module = module;
        _cineMachine = cineMachine;
        _camera = cam;
        _dayNightManager = dayNightManager;
        
        _module.OnVisionRadiusUpdated += UpdateVision;
        _module.OnLightingUpdated += UpdateLighting;
        
        _currentVision = _module.VisionRadius;
        _targetVision = _module.VisionRadius;
        
        ApplyVision(_currentVision);
    }

    private void Update()
    {
        var lightValue = _dayNightManager.Value;
        _visionLight.color = new Color(lightValue, lightValue, lightValue, 1f);

        if (Mathf.Approximately(_currentVision, _targetVision)) return;

        _currentVision = Mathf.MoveTowards(_currentVision, _targetVision, _visionChangeSpeed * Time.deltaTime);

        ApplyVision(_currentVision);
    }

    private void UpdateLighting(float value, bool state) => _lighting.pointLightOuterRadius = state ? value : 0f;

    private void UpdateVision(float value) => _targetVision = value;

    private void ApplyVision(float value)
    {
        _visionCollider.size = new Vector2(value * _camera.aspect, value) * 2f;

        _cineMachine.Lens.OrthographicSize = value;
        _visionLight.pointLightOuterRadius = value * _camera.aspect * 2f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;

        _module.EnterEntity(visible);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;

        _module.ExitObject(visible);
    }

    private void OnDestroy()
    {
        _module.OnLightingUpdated -= UpdateLighting;
        _module.OnVisionRadiusUpdated -= UpdateVision;
    }
}
}