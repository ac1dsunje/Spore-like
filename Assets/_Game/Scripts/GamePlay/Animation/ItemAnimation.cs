using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.GamePlay.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ShadowCaster2D))]
public class ItemAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private ShadowCaster2D _shadowCaster;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _shadowCaster = GetComponent<ShadowCaster2D>();
    }
    
    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
        _animator.enabled = visible;
    }

    public void SetConfig(AnimationConfig config)
    {
        _shadowCaster.enabled = config.CastShadows;
        _renderer.sprite = config.Sprite;
        if (config.Controller)
        {
            _animator.runtimeAnimatorController = config.Controller;
        }
    }
}
}