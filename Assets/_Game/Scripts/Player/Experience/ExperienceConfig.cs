using UnityEngine;

namespace _Game.Scripts.Player.Experience
{
[CreateAssetMenu(fileName = "New Experience Config", menuName = "Configs/Game/Player/Experience")]
public class ExperienceConfig: ScriptableObject
{
    [SerializeField] public float _levelSet = 3;
    [SerializeField] public float _levelScaler = 1.5f;
}
}