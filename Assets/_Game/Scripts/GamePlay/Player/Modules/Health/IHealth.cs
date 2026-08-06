using System;

namespace _Game.Scripts.GamePlay.Player.Modules.Health
{
public interface IHealth
{
    public event Action<float, float> OnHealthChanged;
}
}