using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Buffs
{
public class ActiveBuffSlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void Construct(Buff buff)
    {
        _image.sprite = buff.Sprite;
    }
}
}