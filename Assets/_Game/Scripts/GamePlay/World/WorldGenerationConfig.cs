using System.Collections.Generic;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World
{
[CreateAssetMenu(fileName = "New WorldGen Config", menuName = "Configs/Game/World/Generation")]
public class WorldGenerationConfig: ScriptableObject
{
    [field: SerializeField] public int ChunkSize {get; private set;} = 16;
    [field: SerializeField] public List<BiomeConfig> BiomeConfigs {get; private set;}
    [field: SerializeField] public float Scale { get; private set; } = 0.03f;
    [field: SerializeField] public bool GenerateRandomSeed { get; private set; }
    [field: SerializeField] public int Seed {get; private set;}
}
}