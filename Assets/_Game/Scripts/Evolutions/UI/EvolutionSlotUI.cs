using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Evolutions.UI
{
public class EvolutionSlotUI : MonoBehaviour
{
    [SerializeField] private Image _evolutionImage;
    [SerializeField] private Image _rarityFrameImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    private Evolution _evolution;

    public event Action<Evolution> OnSlotClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void SetBuff(Evolution evolution)
    {
        _evolution = evolution;
        _evolutionImage.sprite = evolution.Sprite;
        _rarityFrameImage.sprite = evolution.Frame;
        _name.text = $"{evolution.Name}";
        _description.text = $"{evolution.Description} {evolution.Value}";
    }

    private void OnButtonClick()
    {
        OnSlotClicked?.Invoke(_evolution);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
}