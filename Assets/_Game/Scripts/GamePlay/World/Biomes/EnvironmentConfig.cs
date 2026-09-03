using _Game.Scripts.GamePlay.Entities;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[CreateAssetMenu(fileName = "New Environment Config", menuName = "Game/World/Biomes/Environment")]
public class EnvironmentConfig: ScriptableObject
{
    [field: SerializeField] public EntityConfig[] FoodItems { get; private set; }
    [field: SerializeField] public int Chance { get; private set; } = 50;
}
}