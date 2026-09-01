using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[Serializable]
public class AnimationSettings
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
    [field: SerializeField] public Color Color { get; private set; } = Color.green;
    [field: SerializeField] public ParticleSystem Prefab { get; private set; }
}
}