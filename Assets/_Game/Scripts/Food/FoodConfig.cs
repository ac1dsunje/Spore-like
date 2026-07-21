using UnityEngine;

namespace _Game.Scripts.Food
{
[CreateAssetMenu(fileName = "New Food Config", menuName = "Configs/Game/World/Food")]
public class FoodConfig: ScriptableObject
{
    [field : SerializeField] public int FeedAmount { get; private set; }
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}
}