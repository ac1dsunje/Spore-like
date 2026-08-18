using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Experience
{
[Serializable]
public class ExperienceConfig
{
    [field: SerializeField] public int LevelSet { get; private set; } = 10;
    [field: SerializeField] public ExperienceServiceConfig[] ExperienceTypes { get; private set; }
}
}