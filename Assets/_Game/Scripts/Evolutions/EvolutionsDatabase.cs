using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{

[CreateAssetMenu(fileName = "New evolution", menuName = "Configs/Game/Evolutions/Database")]
public class EvolutionsDatabase: ScriptableObject
{
    [field: SerializeField] public EvolutionConfig[] Evolutions { get; private set; }
    
    public List<Evolution> GenerateEvolutions()
    {
        var evolutions = new List<Evolution>();
        foreach(var evolution in Evolutions)
        {
            var evo = evolution.CreateEvolution();
            evolutions.Add(evo);
        }
        return evolutions;
    }
}
}