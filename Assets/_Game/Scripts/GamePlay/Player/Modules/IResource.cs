using System;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public interface IResource
{
    public event Action<float, float> OnValueChanged;
}
}