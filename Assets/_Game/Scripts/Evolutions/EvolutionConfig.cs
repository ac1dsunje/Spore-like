using UnityEngine;

namespace _Game.Scripts.Evolutions
{

[CreateAssetMenu(fileName = "New evolution", menuName = "Configs/Game/Evolutions/Evolution")]
public class EvolutionConfig: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float BasicValue { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}