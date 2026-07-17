using UnityEngine;

namespace _Game.Scripts.Player.Movement
{
[CreateAssetMenu(fileName = "New movement config", menuName = "Configs/Game/Player/Movement")]
public class MovementConfig: ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
}
}