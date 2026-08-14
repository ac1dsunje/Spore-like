using _Game.Scripts.GamePlay.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
[CreateAssetMenu(fileName = "New Experience Config", menuName = "Configs/Game/Player/Experience")]
public class ExperienceConfig: ScriptableObject
{
    [field: SerializeField] public int LevelSet { get; private set; } = 5;
    [field: SerializeField] public int LevelScaler { get; private set; } = 1;
    [field: SerializeField] public ExperienceServiceConfig[] ExperienceTypes { get; private set; }
}
}