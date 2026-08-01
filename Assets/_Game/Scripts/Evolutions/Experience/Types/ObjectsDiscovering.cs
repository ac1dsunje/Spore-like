using System;
using System.Collections.Generic;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class ObjectsDiscovering: IEvolutionExperience
{
    private readonly List<GameObject> _discoveredObjects = new();
    
    private readonly PlayerModel _playerModel;

    public event Action<int> OnExperienceGained;
    public ObjectsDiscovering(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.Vision.OnGameObjectDiscovered += OnObjectDiscovered;
    }
    
    private void OnObjectDiscovered(GameObject go)
    {
        if (_discoveredObjects.Contains(go)) return;
        _discoveredObjects.Add(go);
        OnExperienceGained?.Invoke(1);
    }

    public void Dispose()
    {
        _playerModel.Vision.OnGameObjectDiscovered -= OnObjectDiscovered;
    }
}
}