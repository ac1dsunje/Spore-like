using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Experience
{
[Serializable]
public class ExperienceConfig
{
    [field: SerializeField] public int LevelSet { get; private set; } = 10;
    [field: SerializeField] public int Level { get; private set; } = 1;
    [field: SerializeField] public List<ExperienceServiceConfig> ExperienceTypes { get; private set; } = new();
}
}