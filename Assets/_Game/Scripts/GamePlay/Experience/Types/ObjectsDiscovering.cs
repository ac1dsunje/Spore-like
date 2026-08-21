using System.Collections.Generic;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class ObjectsDiscovering: ExperienceService
{
    private readonly VisionModule _module;
    
    private readonly HashSet<GameObject> _discoveredObjects = new();

    public ObjectsDiscovering(VisionModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnGameObjectDiscovered += OnObjectDiscovered;
    }

    private void OnObjectDiscovered(GameObject gameObject)
    {
        if (!_discoveredObjects.Add(gameObject)) return;

        AddAmount(1);
    }

    public override void Dispose() => _module.OnGameObjectDiscovered -= OnObjectDiscovered;
}
}