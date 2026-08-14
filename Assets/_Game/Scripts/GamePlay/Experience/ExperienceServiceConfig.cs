using System;
using _Game.Scripts.GamePlay.Evolutions.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Experience
{
[Serializable]
public class ExperienceServiceConfig
{
    [field: SerializeField] public ExperienceType Type { get; private set; }
    [field: SerializeField] public float Amount { get; private set; } = 1f;
}
}