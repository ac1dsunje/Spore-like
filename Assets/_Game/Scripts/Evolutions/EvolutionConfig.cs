using System.Collections.Generic;
using _Game.Scripts.Evolutions.Experience;
using _Game.Scripts.Evolutions.Stats;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
[CreateAssetMenu(fileName = "NewEvolutionConfig", menuName = "Configs/Game/Evolutions/Evolution")]
public class EvolutionConfig: ScriptableObject
{
    [Header("Visual")]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [Header("Buffs/Debuffs")]
    [field: SerializeField] public List<Stat> Stats { get; private set; }
    [Header("InitialState")]
    [field: SerializeField] public EvolutionState State { get; private set; }
    [Header("References")]
    [field: SerializeField] public EvolutionConfig[] Unlocks { get; private set; }
    [field: SerializeField] public EvolutionConfig[] Blocks { get; private set; }
    [Header("Experience")]
    [field: SerializeField] public int ExperienceForFirstLevel { get; private set; }
    [field: SerializeField] public EvolutionExperienceType ExperienceType { get; private set; }
}
}