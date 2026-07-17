using UnityEngine;

namespace _Game.Scripts.Buffs
{

public enum BuffType
{
    Vision,
    Damage,
    MoveSpeed
}

[CreateAssetMenu(fileName = "New buff", menuName = "Configs/Buffs/Buff")]
public class Buff: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public BuffType Type { get; private set; }
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}