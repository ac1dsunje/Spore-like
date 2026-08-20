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

    private PlayerRegistry _registry;

    [Inject]
    private void Construct(PlayerRegistry registry)
    {
        _registry = registry;
        _registry.OnLocalPlayerAdded += AddPlayer;
    }
    
    private void AddPlayer(PlayerController player)
    {
        _player = player.Model;
        
        _player.Evolutions.OnSlotsFilled += OnSlotsFilled;
        
        _overlayUIScreen.Construct(_player);
        _activeEvolutionsDisplay.Construct(_player.Evolutions);
        _evolutionChooseUIScreen.Construct(_player.Evolutions);
        _activeAbilitiesDisplay.Construct(_player.Abilities);
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
        _player.Evolutions.OnSlotsFilled -= OnSlotsFilled;
        _registry.OnLocalPlayerAdded -= AddPlayer;
    }
}
}