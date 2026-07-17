using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Buffs
{
public class BuffSlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    private Buff _buff;

    public event Action<Buff> OnSlotClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void SetBuff(Buff buff)
    {
        _buff = buff;
        _image.sprite = _buff.Sprite;
        _name.text = $"Buff {_buff.Name}";
        _description.text = $"{_buff.Description} {_buff.Type.ToString()} by {_buff.Value}";
    }

    private void OnButtonClick()
    {
        OnSlotClicked?.Invoke(_buff);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
}