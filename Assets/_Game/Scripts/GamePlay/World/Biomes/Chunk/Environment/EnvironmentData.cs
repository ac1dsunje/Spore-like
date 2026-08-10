using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Biomes.Chunk.Environment
{
[Serializable]
public class EnvironmentData
{
    [field: SerializeField] public GameObject[] Prefabs { get; private set; }
    [field: SerializeField] public float Chance { get; private set; }
}
}