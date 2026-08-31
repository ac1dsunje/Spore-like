using UnityEngine;

namespace _Game.Scripts.GamePlay.Buffs
{
[CreateAssetMenu(fileName = "NewBuffsDatabase", menuName = "Game/Buffs/Database")]
public class BuffsDatabase: ScriptableObject
{
    [field: SerializeField] public BuffConfig[] Buffs { get; private set; }
}
}