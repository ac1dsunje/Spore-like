using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Experience;   
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public enum EntityType
{
    Food = 0,
    Player = 1,
    SeaUrchin = 2
}
[CreateAssetMenu(fileName = "New entity Config", menuName = "Game/Entities/Config")]
public class EntityConfig: ScriptableObject
{
    [field: SerializeField] public EntityType EntityType { get; private set; }
    [field: SerializeField] public StatsConfig EntityStatsConfig { get; private set; }
    [field: SerializeField] public int ExperienceAmount { get; private set; } = 1;
    [field: SerializeField] public EntityExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }
    
}
}