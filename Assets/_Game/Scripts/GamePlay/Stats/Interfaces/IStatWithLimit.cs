using System;

namespace _Game.Scripts.GamePlay.Interfaces
{
public interface IStatWithLimit
{
    public event Action<float, float> OnValueChanged;
}
}