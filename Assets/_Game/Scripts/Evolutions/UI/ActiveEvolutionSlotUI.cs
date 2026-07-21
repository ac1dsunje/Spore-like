using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Evolutions.UI
{
public class ActiveEvolutionSlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    public void Construct(Evolution evolution)
    {
        _image.sprite = evolution.Config.Sprite;
        _frame.sprite = evolution.Frame;
    }
}
}