using System;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public interface IResource
{
    public event Action<float, float> OnValueChanged;
}
}