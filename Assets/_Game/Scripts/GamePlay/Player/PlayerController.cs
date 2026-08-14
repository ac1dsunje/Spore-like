using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Rarities;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: NetworkBehaviour, IDamageAble, IDisguiseAble
{
    [Header("Modules")]
    [field: SerializeField] public ItemAnimation Animation { get; private set; }
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] private AnimationConfig _animationConfig;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private PlayerAuthority _authority;
    [Inject] private ItemAnimation _animation;

    public override void OnNetworkSpawn()
    {
        _authority.SetNetworkType(IsOwner);
        Initialize();
    }

    public void SetSinglePlayer()
    {
        _authority.SetNetworkType(true);
        Initialize();
    }

    private void Initialize()
    {
        Model.Initialize(this);
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        Animation.SetConfig(_animationConfig);
        _playerRegistry.AddPlayer(this);
    }

    public void TakeDamage(HitInfo hit) => Model.TakeDamage(hit);

    public bool SetVisible(float sensorics)
    {
        var show = sensorics >= Model.Disguise.Disguise;
        _animation.SetVisible(show);
        return show;
    }
}
}