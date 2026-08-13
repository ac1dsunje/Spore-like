using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Evolutions;
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

    public override void OnNetworkSpawn()
    {
        Initialize();
    }

    public void Initialize()
    {
        _authority.SetNetworkType(IsOwner);
        Model.Initialize(this);
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        Animation.SetConfig(_animationConfig);
        _playerRegistry.AddPlayer(this);
    }

    public void TakeDamage(HitInfo hit) => Model.TakeDamage(hit);

    public void SetVisible(float sensorics) => Model.SetVisible(sensorics);
}
}