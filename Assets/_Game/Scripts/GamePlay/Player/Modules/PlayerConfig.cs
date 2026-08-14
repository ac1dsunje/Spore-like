using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules
{
[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/Game/Player/Config")]
public class PlayerConfig: ScriptableObject
{
    [field: SerializeField] public StatsConfig[] InitialConfigs { get; private set; }
}
}