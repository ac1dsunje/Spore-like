using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Evolutions
{
public class EvolutionSlotUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    private EvolutionConfig _evolutionConfig;

    public event Action<EvolutionConfig> OnSlotClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void SetBuff(EvolutionConfig evolutionConfig)
    {
        _evolutionConfig = evolutionConfig;
        _image.sprite = _evolutionConfig.Sprite;
        _name.text = $"Evolution {_evolutionConfig.Name}";
        _description.text = $"{_evolutionConfig.Description} {_evolutionConfig.BasicValue}";
    }

    private void OnButtonClick()
    {
        OnSlotClicked?.Invoke(_evolutionConfig);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
}