using _Game.Scripts.GamePlay.Evolutions.UI.Choosing;
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
    [Header("Players")]
    [SerializeField] private PlayerSpawner _playerSpawner;

    [Header("UI")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private OverlayUIScreen _overlayUIScreen;
    [SerializeField] private PauseUIScreen _pauseUIScreen;
    [SerializeField] private EvolutionChooseUIScreen _evolutionChooseUIScreen;

    private void Awake()
    {
        var player = _playerSpawner.Spawn(_ticker);
        
        _overlayUIScreen.Construct(player.Model);
        
        _worldGenerator.Construct(player.transform);
        _camera.Target.TrackingTarget = player.transform;
        
        _evolutionChooseUIScreen.Construct(player.Model.Evolutions);
        _uiManager.Construct(_evolutionChooseUIScreen, _pauseUIScreen, player.Model);
    }
}
}
