using UnityEngine;

namespace _Game.Scripts.Food
{
public class FoodController: MonoBehaviour
{
    [SerializeField] private float _feedAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var eater = other.gameObject.GetComponent<IEater>();
        eater?.Eat(_feedAmount);
        Destroy(gameObject);
    }
}
}