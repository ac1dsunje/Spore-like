using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Vision
{
public class PlayerVision: PlayerNetworkBehaviour
{
    [SerializeField] private BoxCollider2D _visionCollider;
        
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
        if (!IsLocal)
        {
            _visionCollider.enabled = false;
            return;
        }
        _module.OnVisionRadiusChanged += UpdateVisuals;
        UpdateVisuals(_module.VisionRadius);
    }

    private void UpdateVisuals(float radius)
    {
        _visionCollider.size = new Vector2(radius * _camera.aspect, radius) * 2;
        _cineMachine.Lens.OrthographicSize = radius;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        _module.DiscoverGameObject(other.gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_module != null)
        {
            _module.OnVisionRadiusChanged -= UpdateVisuals;
        }
    }
}
}