using UnityEngine;

namespace _Game.Scripts.Food
{
public class FoodItem: MonoBehaviour
{
    [SerializeField] private float _expAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<IEater>(out var eater);
        eater?.Eat(_expAmount);
        Destroy(gameObject);
    }
}
}