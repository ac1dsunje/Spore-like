using UnityEngine;

namespace _Game.Scripts.Evolutions.Rarities
{
[CreateAssetMenu(fileName = "New evolution", menuName = "Configs/Game/Evolutions/Rarities/Rarity")]
public class EvolutionRarityConfig: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float Scaler { get; private set; } = 1;
    [field: SerializeField] public float Chance { get; private set; } = 50f;
}
}