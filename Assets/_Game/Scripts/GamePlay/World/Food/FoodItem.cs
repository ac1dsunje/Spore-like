using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _renderer;

    private FoodConfig _config;
    private float _health;
    public event Action<int> OnDeath;

    public void Construct(FoodConfig config)
    {
        _config = config;
        _renderer.sprite = _config.Sprite;
        if (_config.AnimatorController)
        {
         _animator.runtimeAnimatorController = _config.AnimatorController;
        }

        _health = _config.MaxHealth;
    }

    public void TakeHit(float damage, float penetration)
    {
        var dmg = penetration - _config.Shield >= 0 ? damage : 0;
        if (dmg <= 0) return;
        _health -= dmg;
        SpawnParticles();
        if (_health <= 0f) Die();
    }

    private void SpawnParticles()
    {
        var particles = Instantiate(
            _config.ParticlesPrefab,
            transform.position,
            Quaternion.identity
        );

        var main = particles.main;
        main.startColor = _config.Color;
    }

    private void Die()
    {
        OnDeath?.Invoke(_config.FeedAmount);
        Destroy(gameObject);
    }
}
}