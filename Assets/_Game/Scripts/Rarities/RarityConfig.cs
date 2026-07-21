using UnityEngine;

namespace _Game.Scripts.Rarities
{
[CreateAssetMenu(fileName = "NewRarity", menuName = "Configs/Game/Rarities/Rarity")]
public class RarityConfig: ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float Scaler { get; private set; } = 1;
    [field: SerializeField] public float Chance { get; private set; } = 50f;
}
}