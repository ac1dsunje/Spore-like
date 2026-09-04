using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{ 
[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Game/Projectiles/Projectile")]
public class ProjectileConfig: ScriptableObject
{
    [field: SerializeField, Min(0.1f)] public float HitTime { get; private set; }
    [field: SerializeField] public bool FollowSource { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
}
}