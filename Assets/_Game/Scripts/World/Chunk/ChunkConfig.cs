using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.World.Chunk
{
[CreateAssetMenu(fileName = "New Chunk Config", menuName = "Configs/Game/World/Chunk")]
public class ChunkConfig: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ChunkSize { get; private set; } = 10;
    [field: SerializeField] public FoodConfig[] Foods { get; private set; }
    [field: SerializeField] public float PlantChance { get; private set; }
}
}