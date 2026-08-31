using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Buffs
{
[CreateAssetMenu(fileName = "NewBuffConfig", menuName = "Game/Buffs/Buff")]
public class BuffConfig: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field : SerializeField] public BuffType Type { get; private set; }
    [field: SerializeField] public List<SourceStat> Stats { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}