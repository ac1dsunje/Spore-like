using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
public class FoodItem: MonoBehaviour
{
    [field: SerializeField] private int _feedAmount;
    
    private RarityConfig _rarity;

    public void SetRarity(RaritiesDatabase raritiesData)
    {
        _rarity = raritiesData.GetRandom();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<IEater>(out var eater);
        eater?.Eat(_feedAmount * _rarity.FoodScaler);
        Destroy(gameObject);
    }
}
}