using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Stats
{
[Serializable]
public class Stat
{
    [field: SerializeField] public StatType Type {get; private set;}
    [field: SerializeField] public float Value {get; private set;}
}
}