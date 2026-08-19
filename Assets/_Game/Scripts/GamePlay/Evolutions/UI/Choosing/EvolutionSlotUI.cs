using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.Evolutions.UI.Choosing
{
public class EvolutionSlotUI : MonoBehaviour
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
        _statsDescription.text = GetStatsDescription();
    }
    
    private string GetStatsDescription()
    {
        var text = new StringBuilder();

        foreach (var stat in _evolution.Stats)
        {
            text.Append($"{stat.Type}");
            text.Append(stat.Value > 0 ? " +" : " ");
            text.AppendLine($"{stat.CurrentValue}");
        }

        if (_evolution.Config.Abilities != null)
        {
            foreach (var ability in _evolution.Config.Abilities)
            {
                text.AppendLine($"Grants ability to {ability.Type.ToString()}");
            }
        }

        return text.ToString();
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