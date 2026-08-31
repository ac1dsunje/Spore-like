using UnityEngine;

namespace _Game.Scripts.GamePlay
{
[CreateAssetMenu(fileName = "NewEntityStatsConfig", menuName = "Game/Stats/GeneralConfig")]
public class EntityStatsConfig: ScriptableObject
{
    [field: SerializeField] public StatsConfig[] InitialConfigs { get; private set; }
}
}