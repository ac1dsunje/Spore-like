using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Vision
{
public class PlayerVision: PlayerNetworkBehaviour
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
    }

    private void Update()
    {
        if (!IsLocal) return;
        _visionCollider.size = new Vector2(_module.VisionRadius * _camera.aspect, _module.VisionRadius) * 2;
        _cineMachine.Lens.OrthographicSize = _module.VisionRadius;
        
        _visionLight.pointLightOuterRadius = _module.LightingRadius;
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        _module.DiscoverGameObject(other.gameObject);
    }
}
}