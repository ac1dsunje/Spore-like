using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes.Environment.Enemies
{
public class Enemy: MonoBehaviour, IDamageAble
{
    [SerializeField] private float _damage = 3f;
    [SerializeField] private float _maxHealth = 40f;
    [SerializeField] private float _ignoreResistance;

    private float _health;

    private HitInfo _hit;

    private void Awake()
    {
        _health = _maxHealth;
        _hit = new HitInfo(_damage, _ignoreResistance, this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryAttack(other);
    }

    private void TryAttack(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;
        damageAble.TakeDamage(_hit);
    }

    public void TakeDamage(HitInfo hit)
    {
        _health -= hit.Damage;
        _health = Mathf.Max(0f, _health);
        
        var reflectedHit = new HitInfo(_damage, _ignoreResistance, null);
        
        hit.Owner?.TakeDamage(reflectedHit);
        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
}