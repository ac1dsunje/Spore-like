using System;

namespace _Game.Scripts.GamePlay.World.Food
{
public interface IBiteable
{
    public void TakeByte(float damage, float penetration);
    public event Action<int> OnEaten;
}
}