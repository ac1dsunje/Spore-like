using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.World.Food
{
[RequireComponent(typeof(PolygonCollider2D))]
public class FoodController: MonoBehaviour
{
    [Inject] private ItemAnimation _itemAnimation;
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private EntityStats _stats;
    
    private FoodConfig _config;
    private PolygonCollider2D _collider;

    public event Action<int> OnDeath;

    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    public void Initialize(FoodConfig config)
    {
        _config = config;

        var sprite = config.AnimationConfig.Sprite;

        _itemAnimation.SetConfig(config.AnimationConfig);

        SetColliderShape(sprite);

        _health.OnDamageTaken += SpawnParticles;
        _health.OnDeath += Die;
        
        _stats.AddInitialStats(config.StatsConfig.Stats);
    }

    private void SetColliderShape(Sprite sprite)
    {
        if (!sprite)
        {
            _collider.enabled = false;
            return;
        }

        _collider.enabled = true;

        var shapeCount = sprite.GetPhysicsShapeCount();
        _collider.pathCount = shapeCount;

        var shape = new List<Vector2>();

        for (var i = 0; i < shapeCount; i++)
        {
            shape.Clear();

            sprite.GetPhysicsShape(i, shape);
            _collider.SetPath(i, shape);
        }
    }

    public void TakeHit(float damage, float penetration)
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
        OnDeath?.Invoke(_config.FeedAmount);
        Destroy(gameObject, 1f);
        
        _health.OnDamageTaken -= SpawnParticles;
        _health.OnDeath -= Die;
        
        gameObject.SetActive(false);
    }
}
}