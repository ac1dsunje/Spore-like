using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{

[CreateAssetMenu(fileName = "New evolution", menuName = "Configs/Game/Evolutions/Database")]
public class EvolutionsDatabase: ScriptableObject
{
    [field: SerializeField] public EvolutionConfig[] Evolutions { get; private set; }
    [field: SerializeField] public int BasicChance { get; private set; } = 10;
    [field: SerializeField] public int ChanceScaler { get; private set; } = 5;
    
    public List<Evolution> GenerateEvolutions()
    {
        var evolutions = new List<Evolution>();
        foreach(var evolution in Evolutions)
        {
            var evo = new Evolution(evolution);
            evolutions.Add(evo);
        }
        return evolutions;
    }
}
}