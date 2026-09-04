using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(BoxCollider2D))]
public class VisionHitbox: MonoBehaviour
{
    private BoxCollider2D _collider;
    
    [Inject] private VisionModule _vision;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        CheckRadius();
    }
    
    public void SetSize(Vector2 size)
    {
        _collider.size = size;
        CheckRadius();
    }

    private void CheckRadius()
    {
        _collider.enabled = _vision.VisionRadius > 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;

        _vision.EnterEntity(visible);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;

        _vision.ExitObject(visible);
    }
}
}