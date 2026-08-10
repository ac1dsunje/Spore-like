using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions.Experience
{
[Serializable]
public class EvolutionExperienceConfig
{
    [field: SerializeField] public EvolutionExperienceType Type { get; private set; }
    [field: SerializeField] public float Amount { get; private set; } = 1f;
}
}