using System.Collections.Generic;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Experience;
using UnityEngine;

namespace _Game.Scripts.Player
{
[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/Game/Player/Config")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public List<Stat> Stats { get; private set; }
    [field: SerializeField] public ExperienceConfig  ExperienceConfig { get; private set; }
}
}