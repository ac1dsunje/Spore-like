using _Game.Scripts.Player.Experience;
using _Game.Scripts.Player.Movement;
using UnityEngine;

namespace _Game.Scripts.Player
{
[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/Game/Player")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public MovementConfig  MovementConfig { get; private set; }
    [field: SerializeField] public ExperienceConfig  ExperienceConfig { get; private set; }
}
}