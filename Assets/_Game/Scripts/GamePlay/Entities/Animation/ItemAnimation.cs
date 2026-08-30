using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ItemAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    
    private Sprite _currentSprite;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    
    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
        _animator.enabled = visible;
    }

    public void SetConfig(AnimationSettings settings)
    {
        _renderer.sprite = settings.Sprite;
        if (settings.Controller)
        {
            _animator.runtimeAnimatorController = settings.Controller;
        }
    }
}
}