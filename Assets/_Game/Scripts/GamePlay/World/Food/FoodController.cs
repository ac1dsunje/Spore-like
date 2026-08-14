using System;
using _Game.Scripts.GamePlay.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: MonoBehaviour
{
    [Inject] private ItemAnimation _itemAnimation;
    
    private FoodConfig _config;
    private float _health;
    public event Action<int> OnDeath;

    public void SetConfig(FoodConfig config)
    {
        _config = config;
        _itemAnimation.SetConfig(config.AnimationConfig);

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
        Destroy(gameObject, 1f);
        gameObject.SetActive(false);
    }
}
}