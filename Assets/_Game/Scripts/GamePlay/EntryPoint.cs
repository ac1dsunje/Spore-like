using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI;
using _Game.Scripts.GamePlay.World;
using Unity.Cinemachine;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Ticker _ticker;
    [Header("World")]
    [SerializeField] private WorldGenerator _worldGenerator;
    [Header("Camera")]
    [SerializeField] private CinemachineCamera _camera;
    [Header("Player")]
    [SerializeField] private PlayerController _player;

    [Header("UI")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private OverlayUIScreen _overlayUIScreen;
    [SerializeField] private PauseUIScreen _pauseUIScreen;
    [SerializeField] private EvolutionChooseUIScreen _evolutionChooseUIScreen;

    private void Awake()
    {
        _player.Initialize(_ticker);
        
        _overlayUIScreen.Construct(_player.Model);
        
        _worldGenerator.Construct(_player.transform);
        _camera.Target.TrackingTarget = _player.transform;
        
        _evolutionChooseUIScreen.Construct(_player.Model.Evolutions);
        _uiManager.Construct(_evolutionChooseUIScreen, _pauseUIScreen, _player.Model);
    }
}
}
