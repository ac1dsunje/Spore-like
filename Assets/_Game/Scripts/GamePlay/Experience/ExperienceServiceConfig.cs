using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Experience
{
[Serializable]
public class ExperienceServiceConfig
{
    [field: SerializeField] public ExperienceType Type { get; private set; }
    [field: SerializeField, Range(1f, 100f)] public float Amount { get; private set; } = 1f;
}
}