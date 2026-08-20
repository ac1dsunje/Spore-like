using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI.Choosing
{
public class ChoosingEvolutionSlotUI : MonoBehaviour
{
    [SerializeField] private Image _evolutionImage;
    [SerializeField] private Image _rarityFrameImage;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _statsDescription;

    private Evolution _evolution;

    public event Action<Evolution> OnSlotClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void SetEvolution(Evolution evolution)
    {
        _evolution = evolution;
        _evolutionImage.sprite = evolution.Config.Sprite;
        _rarityFrameImage.sprite = evolution.Frame;
        _name.text = $"{evolution.Name}";
        _statsDescription.text = EvolutionFormatter.FormatDescription(evolution);
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