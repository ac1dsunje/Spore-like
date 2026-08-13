using UnityEngine;

namespace _Game.Scripts.GamePlay.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ItemAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    
    public void SetVisible(bool visible) => _renderer.enabled = visible;

    public void SetConfig(AnimationConfig config)
    {
        _renderer.sprite = config.Sprite;
        if (config.Controller)
        {
            _animator.runtimeAnimatorController = config.Controller;
        }
    }
}
}