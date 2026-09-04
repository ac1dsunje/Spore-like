using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: MonoBehaviour
{
    private EvolutionsModule _evolutionsModule;
    private AbilitiesModule _abilitiesModule;
    private EntitiesRegistry _registry;
    
    [Inject] private PauseUIScreen _pauseUIScreen;
    [Inject] private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    
    [Inject] private OverlayUIScreen _overlayUIScreen;
    [Inject] private ActiveEvolutionsDisplay _activeEvolutionsDisplay;
    [Inject] private ActiveAbilitiesDisplay  _activeAbilitiesDisplay;
    [Inject] private ActiveBuffsDisplay _activeBuffsDisplay;
    [Inject] private BarsPanelUI _barsPanelUI;
    [Inject] private DescriptionUI _descriptionUI;

    [Inject]
    private void Construct(EntitiesRegistry registry)
    {
        _registry = registry;
        _registry.OnPlayerInitialized += AddPlayer;
    }
    
    private void AddPlayer(EntityController player)
    {
        _evolutionsModule = player.Evolutions;
        _abilitiesModule = player.Abilities;
        
        _evolutionsModule.OnSlotsFilled += OnSlotsFilled;
        
        _barsPanelUI.Construct(player);
        
        _activeEvolutionsDisplay.OnEvolutionHovered += _descriptionUI.SetDescription;
        _activeEvolutionsDisplay.OnEvolutionUnhovered += _descriptionUI.Hide;
        
        _activeEvolutionsDisplay.Construct(_evolutionsModule);
        _evolutionChooseUIScreen.Construct(_evolutionsModule);
        _activeAbilitiesDisplay.Construct(_abilitiesModule);
        _activeBuffsDisplay.Construct(player.Buffs);
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