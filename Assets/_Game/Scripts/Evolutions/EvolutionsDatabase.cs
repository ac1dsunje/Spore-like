using UnityEngine;

namespace _Game.Scripts.Evolutions
{

[CreateAssetMenu(fileName = "New evolution", menuName = "Configs/Game/Evolutions/Database")]
public class EvolutionsDatabase: ScriptableObject
{
    [field: SerializeField] public EvolutionConfig[] Evolutions { get; private set; }
}
}