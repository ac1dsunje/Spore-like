using System.Collections.Generic;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class ObjectsDiscovering: EvolutionExperienceService
{
    private readonly HashSet<GameObject> _discoveredObjects = new();

    public ObjectsDiscovering(PlayerModel playerModel) : base(playerModel) => PlayerModel.Vision.OnGameObjectDiscovered += OnObjectDiscovered;

    private void OnObjectDiscovered(GameObject gameObject)
    {
        if (!_discoveredObjects.Add(gameObject)) return;

        RaiseEvent(1);
    }

    public override void Dispose() => PlayerModel.Vision.OnGameObjectDiscovered -= OnObjectDiscovered;
}
}