using System;
using _Game.Scripts.GamePlay.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
[Serializable]
public class PlayerExperienceConfig
{
    [field: SerializeField] public int LevelScaler { get; private set; } = 1;
    [field: SerializeField] public ExperienceConfig ExperienceConfig { get; private set; }
}
}