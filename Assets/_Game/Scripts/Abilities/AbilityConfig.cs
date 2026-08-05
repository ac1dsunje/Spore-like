using UnityEngine;

namespace _Game.Scripts.Abilities
{
[CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Configs/Game/Ability")]
public class AbilityConfig: ScriptableObject
{
    [field: SerializeField] public AbilityType Type { get; private set; }
    [field: SerializeField] public float StartCost { get; private set; }
    [field: SerializeField] public bool HasActivePhase { get; private set; } = false;
    [field: SerializeField] public float InUseCost { get; private set; }
    [field: SerializeField] public KeyCode Key { get; private set; } = KeyCode.LeftShift;
}
}