using System;
using UnityEngine;
using _Game.Scripts.GamePlay.Entities.Configuration;

namespace _Game.Scripts.GamePlay.World.Biomes
{
[Serializable]
public class EnvironmentConfig
{
    [field: SerializeField] public EntityConfig Entity { get; private set; }
    [field: SerializeField] public int Chance { get; private set; } = 50;
}
}