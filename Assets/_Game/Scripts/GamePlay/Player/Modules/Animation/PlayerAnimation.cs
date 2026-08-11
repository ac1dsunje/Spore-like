using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void SetConfig(AnimationConfig config)
    {
        _renderer.sprite = config.Sprite;
        _animator.runtimeAnimatorController = config.Controller;
    }
}
}