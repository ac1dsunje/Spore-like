using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.World.Food
{
[CreateAssetMenu(fileName = "New Food Config", menuName = "Configs/Game/Food/Config")]
public class FoodConfig: ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 10;
    [field: SerializeField] public float Shield { get; private set; }
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;
    [field: SerializeField] public GameObject Particle { get; private set; }
}
}