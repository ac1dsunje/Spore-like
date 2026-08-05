using UnityEngine;

namespace _Game.Scripts.Abilities
{
[CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "Configs/Game/Ability")]
public class AbilityConfig: ScriptableObject
{
    [field: SerializeField] public float StartCost { get; private set; }
    [field: SerializeField] public float InUseCost { get; private set; }
}
}