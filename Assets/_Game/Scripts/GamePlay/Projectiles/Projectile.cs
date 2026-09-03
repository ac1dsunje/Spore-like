using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapons
{
public abstract class Projectile: MonoBehaviour
{
    protected HitInfo HitInfo;
    
    public virtual void SetHit(HitInfo hit)
    {
        HitInfo = hit;
    }
}
}