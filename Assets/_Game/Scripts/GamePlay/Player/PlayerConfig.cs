using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player
{
[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/Game/Player/Config")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public StatsConfig InitialConfig { get; set; }
    [field: SerializeField] public ExperienceConfig  ExperienceConfig { get; private set; }
}
}