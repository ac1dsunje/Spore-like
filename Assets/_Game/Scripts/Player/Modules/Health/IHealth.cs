using System;

namespace _Game.Scripts.Player.Modules.Health
{
public interface IHealth
{
    public event Action<float, float> OnHealthChanged;
}
}