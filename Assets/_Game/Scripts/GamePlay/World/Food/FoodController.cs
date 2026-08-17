using System;
using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodController: MonoBehaviour, IBiteable
{
    [Inject] private ItemAnimation _itemAnimation;
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private EntityStats _stats;
    
    private FoodConfig _config;

    public event Action<int> OnEaten;

    public void Initialize(FoodConfig config)
    {
        _config = config;

        _itemAnimation.SetConfig(config.AnimationConfig);

        _health.OnDamageTaken += SpawnParticles;
        _health.OnDeath += Die;
        
        _stats.AddInitialStats(config.StatsConfig.Stats);
    }

    public void TakeByte(float damage, float penetration)
    {
        var appliedDamage = _defense.ApplyResistance(damage, penetration);
        _health.TakeDamage(appliedDamage);
    }

    private void SpawnParticles(float dmg)
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
        OnEaten?.Invoke(_config.FeedAmount);
        
        _health.OnDamageTaken -= SpawnParticles;
        _health.OnDeath -= Die;
        
        Destroy(gameObject, 1f);
        
        gameObject.SetActive(false);
    }
}
}