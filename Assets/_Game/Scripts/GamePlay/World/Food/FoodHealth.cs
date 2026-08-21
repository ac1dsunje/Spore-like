using System;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
public class FoodHealth: MonoBehaviour, IBiteable
{
    public event Action<int> OnEaten;
    
    private FoodConfig _config;
    
    private HealthModule _health;
    private DefenseModule _defense;

    [Inject]
    private void Construct(HealthModule health, DefenseModule defense)
    {
        _health = health;
        _defense = defense;
        
        _health.OnDamageTaken += SpawnParticles;
        _health.OnDeath += Die;
    }

    public void SetConfig(FoodConfig config) => _config = config;

    public void TakeBite(float damage, float penetration)
    {
        var appliedDamage = _defense.ApplyResistance(damage, penetration);
        _health.TakeDamage(appliedDamage);
    }

    private void SpawnParticles(float dmg)
    {
        var particles = Instantiate(
            _config.ParticlesSettings.Prefab,
            transform.position,
            Quaternion.identity
        );

        var main = particles.main;
        main.startColor = _config.ParticlesSettings.Color;
    }

    private void Die()
    {
        OnEaten?.Invoke(_config.FeedAmount);
        
        Destroy(gameObject, 0.5f);
        
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        _health.OnDamageTaken -= SpawnParticles;
        _health.OnDeath -= Die;
    }
}
}