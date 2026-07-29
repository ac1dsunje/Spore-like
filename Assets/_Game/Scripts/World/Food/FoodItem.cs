using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private RaritiesDatabase _rarities;
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;

    private void Awake()
    {
        FeedAmount *= _rarities.GetRandom().FoodScaler;
    }

    public FoodItem Get() => this;

    public void Release() => Destroy(gameObject);
}
}