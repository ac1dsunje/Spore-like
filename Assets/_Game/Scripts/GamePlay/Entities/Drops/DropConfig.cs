using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
public enum DropType
{
    Food = 0,
    Experience = 1,
}

[CreateAssetMenu(fileName = "New drop Config", menuName = "Game/Drop")]
public class DropConfig: ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public DropType DropType { get; private set; }
}
}