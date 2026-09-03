using System;
using _Game.Scripts.GamePlay.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Experience
{
[Serializable]
public class EntityExperienceConfig
{
    [field: SerializeField, Range(1, 100)] public int LevelScaler { get; private set; } = 1;
    [field: SerializeField] public ExperienceConfig ExperienceConfig { get; private set; }
}
}