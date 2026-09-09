using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Types;
using _Game.Scripts.GamePlay.Drops;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public enum EntityAIType
{
    Entity = 0,
    Player = 1,
}

[CreateAssetMenu(fileName = "NewEntityConfig", menuName = "Game/Entities/Config")]
public class EntityConfig : ScriptableObject
{
    [field: SerializeField] public EntityAIType AIType { get; private set; }
    [field: SerializeField] public List<Stat> Stats { get; private set; } = new();
    [field: SerializeField] public EntityExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public ProjectileConfig Projectile { get; private set; }
    [field: SerializeField] public DropsConfig Drops { get; private set; }
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SynchronizeStats();
    }

    private void SynchronizeStats()
    {
        var enumValues = (StatType[])Enum.GetValues(typeof(StatType));
        var existing = new Dictionary<StatType, Stat>();

        foreach (var stat in Stats)
        {
            existing.TryAdd(stat.Type, stat);
        }

        Stats.Clear();

        foreach (var type in enumValues)
        {
            Stats.Add(existing.TryGetValue(type, out var stat) ? stat : new Stat(type, 0f));
        }
    }
#endif
}
}