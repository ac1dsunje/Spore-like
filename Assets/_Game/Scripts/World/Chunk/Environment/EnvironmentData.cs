using System;
using UnityEngine;

namespace _Game.Scripts.World.Chunk.Environment
{
[Serializable]
public class EnvironmentData
{
    [field: SerializeField] public GameObject[] Prefabs { get; private set; }
    [field: SerializeField] public float Chance { get; private set; }
}
}