using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[Serializable]
public class ParticlesSettings
{
    [field: SerializeField] public Color Color { get; private set; } = Color.green;
    [field: SerializeField] public ParticleSystem Prefab { get; private set; }
}
}