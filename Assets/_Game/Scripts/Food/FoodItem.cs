using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private float _expAmount = 1;
    [SerializeField] private RaritiesDatabase _raritiesData;

    private RarityConfig _rarity;

    private void Awake()
    {
        _rarity= _raritiesData.GetRandom();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<IEater>(out var eater);
        eater?.Eat(_expAmount * _rarity.Scaler);
        Destroy(gameObject);
    }
}
}