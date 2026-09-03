using _Game.Scripts.GamePlay.Interfaces;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public interface IHealthController
{
    public void TakeDamage(HitInfo hitInfo);
}
}