using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
[RequireComponent(typeof(BoxCollider2D))]
public class EntityVisionHitbox: MonoBehaviour
{
    private BoxCollider2D _visionCollider;
    
    [Inject] private VisionModule _module;

    private void Start()
    {
        CheckRadius();
    }
    
    public void SetSize(Vector2 size)
    {
        if (_visionCollider == null) _visionCollider = GetComponent<BoxCollider2D>();
        _visionCollider.size = size;
        CheckRadius();
    }

    private void CheckRadius()
    {
        _visionCollider.enabled = _module.VisionRadius != 0;
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