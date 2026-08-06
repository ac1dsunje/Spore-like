using System.Collections.Generic;
using _Game.Scripts.Abilities;
using _Game.Scripts.Evolutions.Experience;
using _Game.Scripts.Stats;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
[CreateAssetMenu(fileName = "NewEvolutionConfig", menuName = "Configs/Game/Evolutions/Evolution")]
public class EvolutionConfig: ScriptableObject
{
    // Creature
    [field: SerializeField] public CreatureType CreatureType { get; private set; }
    // Visual
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    // Buffs/Debuffs
    [field: SerializeField] public List<Stat> Stats { get; private set; }
    // InitialState
    [field: SerializeField] public EvolutionState State { get; private set; }
    // References
    [field: SerializeField] public EvolutionConfig[] Requires { get; private set; }
    [field: SerializeField] public EvolutionConfig[] Blocks { get; private set; }
    // Ability
    [field: SerializeField] public AbilityConfig[] Abilities { get; private set; }
    
    // Experience
    [field: SerializeField] public int ExperienceForFirstLevel { get; private set; }
    [field: SerializeField] public EvolutionExperienceType[] ExperienceTypes { get; private set; }
}
}