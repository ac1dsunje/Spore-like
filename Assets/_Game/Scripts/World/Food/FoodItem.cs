using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
public class FoodItem: MonoBehaviour
{
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;
    
    private RarityConfig _rarity;

    public void SetRarity(RaritiesDatabase raritiesData)
    {
        _rarity = raritiesData.GetRandom();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.TryGetComponent<IEater>(out var eater);
        eater?.Eat(FeedAmount * _rarity.FoodScaler, this);
    }

    public void Release()
    {
        Destroy(gameObject);
    }
}
}