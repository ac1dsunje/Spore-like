using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Types.Vision.CompoundEyes
{
public class CompoundEyes: Evolution
{
    private readonly List<GameObject> _discoveredObjects = new();
    
    public CompoundEyes(EvolutionConfig config) : base(config) {}

    public override void Apply()
    {
        base.Apply();
        Player.Vision.OnGameObjectDiscovered += OnGameObjectDiscovered;
    }

    private void OnGameObjectDiscovered(GameObject gameObject)
    {
        if (_discoveredObjects.Contains(gameObject)) return;
        
        _discoveredObjects.Add(gameObject);
        UpdateExperience(1);
    }

    public override void Dispose()
    {
        if (Player == null) return;
            
        Player.Vision.OnGameObjectDiscovered -= OnGameObjectDiscovered;
    }
}
}