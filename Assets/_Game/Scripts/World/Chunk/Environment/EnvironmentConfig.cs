using UnityEngine;

namespace _Game.Scripts.World.Chunk.Environment
{
[CreateAssetMenu(fileName = "New Environment Config", menuName = "Configs/Game/World/Chunk/Environment")]
public class EnvironmentConfig: ScriptableObject
{
    [field: SerializeField] public EnvironmentData Plants { get; private set; }
    [field: SerializeField] public EnvironmentData Spikes { get; private set; }
}
}