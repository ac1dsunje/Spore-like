using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Animation
{
[Serializable]
public class AnimationSettings
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
}
}