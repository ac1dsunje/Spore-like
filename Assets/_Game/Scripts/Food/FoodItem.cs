using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private RaritiesDatabase _raritiesData;

    private FoodConfig _config;
    
    private RarityConfig _rarity;

    public void SetConfig(FoodConfig config)
    {
        _config = config;
        
        _rarity = _raritiesData.GetRandom();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _config.Sprites[_rarity.Index - 1];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<IEater>(out var eater);
        eater?.Eat(_config.FeedAmount * _rarity.FoodScaler);
        Destroy(gameObject);
    }
}
}