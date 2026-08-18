using System.Collections.Generic;
using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions
{
[CreateAssetMenu(fileName = "NewEvolutionConfig", menuName = "Configs/Game/Evolutions/Evolution")]
public class EvolutionConfig: ScriptableObject
{
    [field: SerializeField] public EvolutionState State { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public CreatureType CreatureType { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public List<EvolutionStat> Stats { get; private set; }
    [field: SerializeField] public AbilityConfig[] Abilities { get; private set; }
    [field: SerializeField] public EvolutionConfig[] Requires { get; private set; }
    [field: SerializeField] public EvolutionConfig[] Blocks { get; private set; }
    [field: SerializeField] public ExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}