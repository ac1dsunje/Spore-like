using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[CreateAssetMenu(fileName = "New Environment Config", menuName = "Configs/Game/World/Biomes/Environment")]
public class EnvironmentConfig: ScriptableObject
{
    [field: SerializeField] public GameObject[] Prefabs { get; private set; }
    [field: SerializeField] public int Chance { get; private set; } = 50;
}
}