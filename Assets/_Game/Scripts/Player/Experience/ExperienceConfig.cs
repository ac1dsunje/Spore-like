using UnityEngine;

namespace _Game.Scripts.Player.Experience
{
[CreateAssetMenu(fileName = "New Experience Config", menuName = "Configs/Game/Player/Experience")]
public class ExperienceConfig: ScriptableObject
{
    [field: SerializeField] public float LevelSet { get; private set; } = 3;
    [field: SerializeField] public float LevelScaler { get; private set; } = 1.5f;
}
}