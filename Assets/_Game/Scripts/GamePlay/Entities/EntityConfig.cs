using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Experience;   
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{

public enum EntityAI
{
    Food = 0,
    Player = 1,
    SeaUrchin = 2,
}

public enum EntityHealth
{
    Basic = 0,
    Reflective = 1,
}

public enum EntityRegeneration
{
    Disabled = 0,
    Enabled = 1
}

public enum EntityAttack
{
    Basic = 0,
    Player = 1,
}

public enum EntityDeath
{
    Basic = 0,
    Player = 1
}

[CreateAssetMenu(fileName = "New entity Config", menuName = "Game/Entities/Config")]
public class EntityConfig: ScriptableObject
{
    [field: SerializeField] public EntityAI AIType { get; private set; }
    [field: SerializeField] public EntityHealth HealthType { get; private set; }
    [field: SerializeField] public EntityRegeneration RegenerationType { get; private set; }
    [field: SerializeField] public EntityAttack AttackType { get; private set; }
    [field: SerializeField] public EntityDeath DeathType { get; private set; }
    [field: SerializeField] public StatsConfig EntityStatsConfig { get; private set; }
    [field: SerializeField] public EntityExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }
    
}
}