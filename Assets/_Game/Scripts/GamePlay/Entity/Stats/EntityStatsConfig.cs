using UnityEngine;

namespace _Game.Scripts.GamePlay.Entity
{
[CreateAssetMenu(fileName = "NewEntityStatsConfig", menuName = "Configs/Game/Stats/GeneralConfig")]
public class EntityStatsConfig: ScriptableObject
{
    [field: SerializeField] public StatsConfig[] InitialConfigs { get; private set; }
}
}