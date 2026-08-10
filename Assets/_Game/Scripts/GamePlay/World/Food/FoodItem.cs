using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodItem: MonoBehaviour
{
    private FoodConfig _config;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _particlePrefab;

    private int _feedAmount;
    public event Action<int> OnDeath;
    
    private float _health;
    private float _shield;

    public void Construct(FoodConfig config)
    {
        _config = config;
        _renderer.sprite = _config.Sprite;
        if (_config.AnimatorController)
        {
         _animator.runtimeAnimatorController = _config.AnimatorController;
        }

        _health = _config.MaxHealth;
        _shield = _config.Shield;
        _feedAmount = _config.FeedAmount;
    }

    public void TakeHit(float damage, float penetration)
    {
        var dmg = penetration - _shield >= 0 ? damage : 0;
        if (dmg <= 0) return;
        _health -= dmg;
        SpawnParticles(_particlePrefab, transform);
        if (_health <= 0f) Die();
    }

    private void SpawnParticles(GameObject particle, Transform parent)
    {
        var particles = Instantiate(particle, parent.position, Quaternion.identity, parent).GetComponent<ParticleSystem>();
        particles.startColor = _config.Color;
    }

    private void Die()
    {
        OnDeath?.Invoke(_feedAmount);
        Destroy(gameObject);
    }
}
}