using _Game.Scripts.GamePlay.CameraManager;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerVision : MonoBehaviour
{
    [SerializeField] private float _visionChangeSpeed = 5f;
    
    private VisionModule _module;
    private CameraController _camController;
    private EntityVisionHitbox _visionHitbox;
    
    private float _targetVision;
    private float _currentVision;

    [Inject]
    private void Construct(VisionModule module, EntityVisionHitbox visionHitbox, CameraController camController)
    {
        _module = module;
        _visionHitbox = visionHitbox;
        _camController = camController;
        
        _module.OnVisionRadiusUpdated += UpdateVision;
        _module.OnEntityDiscovered += ShowEntity;
        
        _currentVision = _module.VisionRadius;
        _targetVision = _module.VisionRadius;
        
        ApplyVision(_currentVision);
    }

    private void Update()
    {
        if (Mathf.Approximately(_currentVision, _targetVision)) return;

        _currentVision = Mathf.MoveTowards(_currentVision, _targetVision, _visionChangeSpeed * Time.deltaTime);

        ApplyVision(_currentVision);
    }

    private void ShowEntity(IVisible entity, bool state)
    {
        entity.SetVisible(state);
    }

    private void UpdateVision(float value) => _targetVision = value;

    private void ApplyVision(float value)
    {
        _visionHitbox?.SetSize(new Vector2(_camController.Aspect, 1f) * (value * 2f));

        _camController.SetSize(value);
    }

    private void OnDestroy()
    {
        _module.OnVisionRadiusUpdated -= UpdateVision;
        _module.OnEntityDiscovered -= ShowEntity;
    }
}
}