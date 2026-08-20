using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI.Bar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class OverlayUIScreen: UIScreen
{
    [SerializeField] private BarUI _healthBarUI;
    [SerializeField] private BarUI _experienceBarUI;
    [SerializeField] private BarUI _enduranceBarUI;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TextMeshProUGUI _descriptionName;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    [Inject] private ActiveEvolutionsDisplay _activeEvolutionsDisplay;
    
    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);

        _activeEvolutionsDisplay.OnEvolutionHovered += SetDescriptionText;
    }

    private void SetDescriptionText(Sprite image, string itemName, string description)
    {
        _descriptionImage.sprite = image;
        _descriptionName.text = itemName;
        _descriptionText.text = description;
    }

    private void OnDestroy()
    {
        _activeEvolutionsDisplay.OnEvolutionHovered -= SetDescriptionText;
    }
}
}