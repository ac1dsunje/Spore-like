using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EntityAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;

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
        SetSprite(settings.Sprite);
        SetAnimator(settings.Controller);
    }

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}