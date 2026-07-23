using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
public class FoodItem: MonoBehaviour
{
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;
    
    private RarityConfig _rarity;

    public void SetRarity(RarityConfig rarity)
    {
        _rarity = rarity;
        FeedAmount *= _rarity.FoodScaler;
    }

    public FoodItem Get() => this;

    public void Release() => Destroy(gameObject);
}
}