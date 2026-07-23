using _Game.Scripts.Rarities;
using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.World.Chunk.Environment.Plants
{
public class Plant: MonoBehaviour
{
    private RarityConfig _rarity;
    private FoodItem _food;
    
    private void Awake()
    {
        TryGetComponent<FoodItem>(out var item);
        if (item)
        {
            _food = item;
        }
    }
    
    public void SetRarity(RaritiesDatabase raritiesData)
    {
        _rarity = raritiesData.GetRandom();
        _food?.SetRarity(_rarity);
    }
}
}