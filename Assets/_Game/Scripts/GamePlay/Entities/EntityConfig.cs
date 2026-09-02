using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
[CreateAssetMenu(fileName = "New entity Config", menuName = "Game/Entities/Config")]
public class EntityConfig: ScriptableObject
{
    [field: SerializeField] public EntityStatsConfig EntityStatsConfig { get; private set; }
    [field: SerializeField] public int ExperienceAmount { get; private set; } = 1;
    [field: SerializeField] public EntityExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }
    
}
}