using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
public abstract class BarUI: MonoBehaviour
{
    [SerializeField] protected Image _bar;

    protected virtual void UpdateBar(float min, float max)
    {
        _bar.fillAmount = min/max;
    }
}
}