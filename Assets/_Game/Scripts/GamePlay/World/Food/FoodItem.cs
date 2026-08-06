using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private FoodConfig _config;

    private int _feedAmount;
    public event Action<int> OnDeath;
    
    private float _health;
    private float _shield;

    private void Awake()
    {
        _health = _config.MaxHealth;
        _shield = _config.Shield;
        _feedAmount = _config.FeedAmount;
    }

    public void TakeHit(float damage, float penetration)
    {
        var dmg = penetration - _shield >= 0 ? damage : 0;
        if (dmg <= 0) return;
        _health -= dmg;
        SpawnParticles(_config.Particle, transform);
        if (_health <= 0f) Die();
    }

    private void SpawnParticles(GameObject particle, Transform parent)
    {
        Instantiate(particle, parent.position, Quaternion.identity, parent);
    }

    private void Die()
    {
        OnDeath?.Invoke(_feedAmount);
        Destroy(gameObject);
    }
}
}