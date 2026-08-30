using System.Collections.Generic;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.UI
{
public class UIManager: MonoBehaviour
{
    private PlayerModel _player;
    
    [Inject] private PauseUIScreen _pauseUIScreen;
    [Inject] private EvolutionChooseUIScreen _evolutionChooseUIScreen;
    
    [Inject] private OverlayUIScreen _overlayUIScreen;
    [Inject] private ActiveEvolutionsDisplay _activeEvolutionsDisplay;
    [Inject] private ActiveAbilitiesDisplay  _activeAbilitiesDisplay;
    [Inject] private ActiveBuffsDisplay _activeBuffsDisplay;
    [Inject] private BarsPanelUI _barsPanelUI;
    [Inject] private DescriptionUI _descriptionUI;
    
    private PlayerRegistry _registry;

    [Inject]
    private void Construct(PlayerRegistry registry)
    {
        _registry = registry;
        _registry.OnPlayerInitialized += AddPlayer;
    }
    
    private void AddPlayer(PlayerController player)
    {
        _player = player.Model;
        
        _player.Evolutions.OnSlotsFilled += OnSlotsFilled;
        
        _barsPanelUI.Construct(_player);
        
        _activeEvolutionsDisplay.OnEvolutionHovered += _descriptionUI.SetDescription;
        _activeEvolutionsDisplay.OnEvolutionUnhovered += _descriptionUI.Hide;
        
        _activeEvolutionsDisplay.Construct(_player.Evolutions);
        _evolutionChooseUIScreen.Construct(_player.Evolutions);
        _activeAbilitiesDisplay.Construct(_player.Abilities);
        _activeBuffsDisplay.Construct(_player.Buffs);
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
        if (_player != null) 
            _player.Evolutions.OnSlotsFilled -= OnSlotsFilled;
        
        _activeEvolutionsDisplay.OnEvolutionHovered -= _descriptionUI.SetDescription;
        _activeEvolutionsDisplay.OnEvolutionUnhovered -= _descriptionUI.Hide;
        
        _registry.OnPlayerInitialized -= AddPlayer;
    }
}
}