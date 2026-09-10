using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager : MonoBehaviour
{
    private EvolutionsModule _evolutionsModule;
    private AbilitiesModule _abilitiesModule;
    private EntitiesRegistry _registry;
    
    private PauseUIScreen _pauseUIScreen;
    private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    private OverlayUIScreen _overlayUIScreen;
    private ActiveEvolutionsDisplay _activeEvolutionsDisplay;
    private ActiveAbilitiesDisplay  _activeAbilitiesDisplay;
    private ActiveBuffsDisplay _activeBuffsDisplay;
    private BarsPanelUI _barsPanelUI;
    private DescriptionUI _descriptionUI;

    [Inject]
    private void Construct(EntitiesRegistry registry, PauseUIScreen pauseUIScreen,  EvolutionChooseUIScreen evolutionChooseUIScreen,
        OverlayUIScreen overlayUIScreen, ActiveEvolutionsDisplay activeEvolutionsDisplay, ActiveAbilitiesDisplay activeAbilitiesDisplay,
        ActiveBuffsDisplay activeBuffsDisplay, BarsPanelUI barsPanelUI, DescriptionUI descriptionUI)
    {
        _pauseUIScreen = pauseUIScreen;
        _evolutionChooseUIScreen = evolutionChooseUIScreen;
        _overlayUIScreen = overlayUIScreen;
        _activeEvolutionsDisplay = activeEvolutionsDisplay;
        _activeAbilitiesDisplay = activeAbilitiesDisplay;
        _activeBuffsDisplay = activeBuffsDisplay;
        _barsPanelUI = barsPanelUI;
        _descriptionUI = descriptionUI;
        
        _registry = registry;
        _registry.OnPlayerInitialized += AddPlayer;
    }
    
    private void AddPlayer(EntityScope player)
    {
        _evolutionsModule = player.Get<EvolutionsModule>();
        _abilitiesModule = player.Get<AbilitiesModule>();
        
        _evolutionsModule.OnSlotsFilled += OnSlotsFilled;
        
        _barsPanelUI.Construct(player);
        
        _activeEvolutionsDisplay.OnEvolutionHovered += _descriptionUI.SetDescription;
        _activeEvolutionsDisplay.OnEvolutionUnhovered += _descriptionUI.Hide;
        
        _activeEvolutionsDisplay.Construct(_evolutionsModule);
        _evolutionChooseUIScreen.Construct(_evolutionsModule);
        _activeAbilitiesDisplay.Construct(_abilitiesModule);
        _activeBuffsDisplay.Construct(player.Get<BuffsModule>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseUIScreen.ToggleScreen();
        }
    }

    private void OnSlotsFilled(List<Evolution> evolutions)
    {
        _evolutionChooseUIScreen.ShowScreen();
    }

    public void OnDestroy()
    {
        if (_evolutionsModule != null) 
            _evolutionsModule.OnSlotsFilled -= OnSlotsFilled;
        
        _activeEvolutionsDisplay.OnEvolutionHovered -= _descriptionUI.SetDescription;
        _activeEvolutionsDisplay.OnEvolutionUnhovered -= _descriptionUI.Hide;
        
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}