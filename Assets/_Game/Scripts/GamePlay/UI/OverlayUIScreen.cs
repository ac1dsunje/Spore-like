using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using Unity.Netcode;
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

    [SerializeField] private Button _host;
    [SerializeField] private Button _connect;
    
    [Inject] private NetworkManager _networkManager;

    private void Start()
    {
        _host.onClick.AddListener(() => _networkManager.StartHost());
        _connect.onClick.AddListener(() => _networkManager.StartClient());
    }
    
    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);
    }

    private void OnDestroy()
    {
        _host.onClick.RemoveAllListeners();
        _connect.onClick.RemoveAllListeners();
    }
}
}