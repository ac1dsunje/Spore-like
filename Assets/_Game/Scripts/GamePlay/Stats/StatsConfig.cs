using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
[CreateAssetMenu(fileName = "NewStatsConfig", menuName = "Game/Stats/Config")]
public class StatsConfig: ScriptableObject
{
    [field: SerializeField] public List<Stat> Stats { get; private set; }
}
}