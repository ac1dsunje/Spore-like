using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Drops
{
[Serializable]
public class DropsConfig
{
    [field: SerializeField] public List<DropConfig> Drops { get; private set; }
}
}