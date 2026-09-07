using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class SocialModule: StatModule
{
    public float Influence { get; private set; }

    private readonly HashSet<ISocial> _objectsInVision = new();

    protected override void Configure()
    {
        BindStat(StatType.Influence, UpdateInfluence);
    }

    public EntityModel GetEntityWithHighestInfluence()
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

            if (social.GetInfluence() > highest)
            {
                result = social;
            }
        }
        return result?.GetEntityModel();
    }

    private void UpdateInfluence(float value) => Influence = value;

    public void EnterEntity(ISocial social) => _objectsInVision.Add(social);

    public void ExitEntity(ISocial visible) => _objectsInVision.Remove(visible);
}
}