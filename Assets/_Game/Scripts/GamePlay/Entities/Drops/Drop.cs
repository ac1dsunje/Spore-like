using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
[RequireComponent(typeof(SpriteRenderer))]
public class Drop: MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private DropConfig _config;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetConfig(DropConfig config)
    {
        _config = config;
        _spriteRenderer.sprite = config.Sprite;
    }
    
    public DropType GetDropType()
    {
        return _config.DropType;
    }
}
}