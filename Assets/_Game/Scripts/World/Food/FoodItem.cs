using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private FoodConfig _config;

    public bool IsAlive => _health > 0f;
    public int FeedAmount { get; private set; }
    
    private RarityConfig _rarity;
    private float _health;
    private float _shield;

    private void Awake()
    {
        _rarity = _config.Rarities.GetRandom();
        _health = _config.MaxHealth * _rarity.FoodScaler;
        _shield = _config.Shield * _rarity.FoodScaler;
        FeedAmount = _config.FeedAmount * _rarity.FoodScaler;
    }

    public void TakeHit(float damage, float penetration)
    {
        var dmg = penetration >= _shield ? damage : 0;
        if (dmg <= 0) return;
        _health -= damage;
        Instantiate(_config.Particle, transform.position, Quaternion.identity, transform);
        if (!IsAlive) Destroy(gameObject);
    }
}
}