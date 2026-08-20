using _Game.Scripts.Core.UI;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.UI.Bar;
using TMPro;
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
    [SerializeField] private Button _player;

    [SerializeField] private Image _descriptionImage;
    [SerializeField] private TextMeshProUGUI _descriptionName;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    [Inject] private NetworkManager _networkManager;
    [Inject] private PlayerSpawner _playerSpawner;
    
    [Inject] private ActiveEvolutionsDisplay _activeEvolutionsDisplay;

    private void Start()
    {
        _host.onClick.AddListener(() => _networkManager.StartHost());
        _connect.onClick.AddListener(() => _networkManager.StartClient());
        _player.onClick.AddListener(_playerSpawner.Spawn);
    }
    
    public void Construct(PlayerModel player)
    {
        _healthBarUI.Construct(player.Health);
        _experienceBarUI.Construct(player.Experience);
        _enduranceBarUI.Construct(player.Endurance);

        _activeEvolutionsDisplay.OnEvolutionClicked += SetDescriptionText;
    }

    private void SetDescriptionText(Sprite image, string itemName, string description)
    {
        _descriptionImage.sprite = image;
        _descriptionName.text = itemName;
        _descriptionText.text = description;
    }

    private void OnDestroy()
    {
        _host.onClick.RemoveAllListeners();
        _connect.onClick.RemoveAllListeners();
        _player.onClick.RemoveAllListeners();
        
        _activeEvolutionsDisplay.OnEvolutionClicked -= SetDescriptionText;
    }
}
}