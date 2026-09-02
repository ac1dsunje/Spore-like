using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(BoxCollider2D))]
public class VisionHitbox: MonoBehaviour
{
    [SerializeField] private BoxCollider2D _visionCollider;
    
    [Inject] private VisionModule _module;

    private void Start()
    {
        CheckRadius();
    }
    
    public void SetSize(Vector2 size)
    {
        _visionCollider.size = size;
        CheckRadius();
    }

    private void CheckRadius()
    {
        _visionCollider.enabled = _module.VisionRadius > 0f;
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
}
}