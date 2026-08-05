using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
public class ActiveAbilitySlotUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    public void Construct(AbilityConfig config)
    {
        _image.sprite = config.Sprite;
        _buttonText.text = config.Key.ToString();
    }
}
}