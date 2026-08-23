using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[CreateAssetMenu(fileName = "New Biome Config", menuName = "Configs/Game/World/Biomes/Biome")]
public class BiomeConfig: ScriptableObject
{
    [field: SerializeField] public float Temperature { get; private set; }
    [field: SerializeField] public float PassAbility { get; private set; }
    [field: SerializeField] public float OxygenBreathing { get; private set; }
    [field: SerializeField] public float HydrogenBreathing { get; private set; }
    [field: SerializeField] public List<SourceStat> AffectedStats { get; private set; } = new();
    [field: SerializeField] public TileBase Tile { get; private set; }
    [field: SerializeField] public List<EnvironmentConfig> EnvironmentConfigs { get; private set; }
    [field: SerializeField] public int ChanceEnvironment { get; private set; } = 20;
}
}