using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public abstract class EvolutionConfig: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float BasicValue { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [field: SerializeField] public EvolutionState State { get; private set; }
    
    [field: SerializeField] public EvolutionConfig[] Unlocks { get; private set; }
    [field: SerializeField] public EvolutionConfig[] Blocks { get; private set; }
    
    public abstract Evolution CreateEvolution();
}
}