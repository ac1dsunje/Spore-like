using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Animation
{
[Serializable]
public class ParticlesConfig
{
    [field: SerializeField] public Color Color { get; private set; } = Color.green;
    [field: SerializeField] public ParticleSystem Prefab { get; private set; }
}
}