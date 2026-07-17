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

    private int _buffIndex;

    public event Action<int> OnSlotClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void SetBuff(int buffIndex)
    {
        //ToDo: set buff config here
        _buffIndex = buffIndex;
        _name.text = $"Buff {buffIndex}";
        _description.text = $"description of buff {buffIndex}";
    }

    private void OnButtonClick()
    {
        OnSlotClicked?.Invoke(_buffIndex);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
}