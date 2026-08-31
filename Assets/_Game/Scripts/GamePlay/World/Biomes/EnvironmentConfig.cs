using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[CreateAssetMenu(fileName = "New Environment Config", menuName = "Game/World/Biomes/Environment")]
public class EnvironmentConfig: ScriptableObject
{
    [field: SerializeField] public FoodConfig[] FoodItems { get; private set; }
    [field: SerializeField] public int Chance { get; private set; } = 50;
}
}