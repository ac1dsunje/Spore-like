using System.Linq;
using UnityEngine;

namespace _Game.Scripts.Rarities
{
[CreateAssetMenu(fileName = "NewRarityDatabase", menuName = "Configs/Game/Rarities/Database")]
public class RaritiesDatabase: ScriptableObject
{
    [field: SerializeField] public RarityConfig[] Rarities { get; private set; }
    
    public RarityConfig GetRandom()
    {
        var randomValue = GetRandomValue();

        var currentWeight = 0f;

        foreach (var rarity in Rarities)
        {
            currentWeight += rarity.Chance;

            if (randomValue <= currentWeight)
            {
                return rarity;
            }
        }

        return Rarities[Rarities.Length - 1];
    }

    private float GetRandomValue()
    {
        return Random.Range(0f, GetTotalWeight());
    }

    private float GetTotalWeight()
    {
        return Rarities.Sum(rarity => rarity.Chance);
    }
}
}