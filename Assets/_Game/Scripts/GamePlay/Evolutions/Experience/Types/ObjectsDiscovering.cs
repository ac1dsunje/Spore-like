using System.Collections.Generic;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class ObjectsDiscovering: EvolutionExperienceService
{
    private readonly HashSet<GameObject> _discoveredObjects = new();

    public ObjectsDiscovering(PlayerModel playerModel, float amount) : base(playerModel, amount) => PlayerModel.Vision.OnGameObjectDiscovered += OnObjectDiscovered;

    private void OnObjectDiscovered(GameObject gameObject)
    {
        if (!_discoveredObjects.Add(gameObject)) return;

        AddAmount(1);
    }

    public override void Dispose() => PlayerModel.Vision.OnGameObjectDiscovered -= OnObjectDiscovered;
}
}