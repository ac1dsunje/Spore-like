using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Drops;
using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Projectiles;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
[CreateAssetMenu(fileName = "New entity Config", menuName = "Game/Entities/Config")]
public class EntityConfig: ScriptableObject
{
    [field: SerializeField] public EntityData Data { get; private set; }
    [field: SerializeField] public StatsConfig EntityStatsConfig { get; private set; }
    [field: SerializeField] public EntityExperienceConfig ExperienceConfig { get; private set; }
    [field: SerializeField] public ProjectileConfig Projectile { get; private set; }
    [field: SerializeField] public DropsConfig Drops { get; private set; }
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }
}

}