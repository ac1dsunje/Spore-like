using UnityEngine;

namespace _Game.Scripts.Player
{
[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/Game/PlayerConfig")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
}
}