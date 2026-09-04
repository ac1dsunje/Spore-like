using _Game.Scripts.GamePlay.Entities.Hitboxes;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
[RequireComponent(typeof(SpriteRenderer))]
public class FoodDrop: MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out PickerHitbox mouth)) return;
        mouth.GetFood();
        Destroy(gameObject);
    }
}
}