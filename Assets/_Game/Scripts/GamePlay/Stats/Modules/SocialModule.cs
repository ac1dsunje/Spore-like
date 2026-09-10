using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class SocialModule : StatModule
{
    public float Influence { get; private set; }

    private readonly HashSet<ISocial> _objectsInVision = new();

    protected override void Configure()
    {
        BindStat(StatType.Influence, UpdateInfluence);
    }

    public Transform GetEntityWithHighestInfluence()
    {
        ISocial result = null;
        float highest = 0;
        
        foreach (var social in _objectsInVision)
        {
            if (result == null)
            {
                result = social;
                highest = social.GetInfluence();
                continue;
            }

            var temp = social.GetInfluence();
            if (temp > highest)
            {
                result = social;
                highest = temp;
            }
        }
        return result?.GetTransform();
    }

    private void UpdateInfluence(float value) => Influence = value;

    public void EnterEntity(ISocial social) => _objectsInVision.Add(social);

    public void ExitEntity(ISocial visible) => _objectsInVision.Remove(visible);
}
}