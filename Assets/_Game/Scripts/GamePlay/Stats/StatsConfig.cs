using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entity;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
[CreateAssetMenu(fileName = "NewStatsConfig", menuName = "Configs/Game/Stats/Config")]
public class StatsConfig: ScriptableObject
{
    [field: SerializeField] public List<Stat> Stats { get; private set; }
}
}