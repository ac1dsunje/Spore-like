using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Abilities
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