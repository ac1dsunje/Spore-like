using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Stats
{
[CreateAssetMenu(fileName = "NewStatsConfig", menuName = "Configs/Game/Stats/Config")]
public class StatsConfig: ScriptableObject
{
    [field: SerializeField] public List<Stat> Stats { get; private set; }
}
}