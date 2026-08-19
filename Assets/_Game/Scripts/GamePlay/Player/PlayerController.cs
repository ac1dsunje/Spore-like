using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Entity.Interfaces;
using _Game.Scripts.GamePlay.Entity.Network;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Rarities;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: NetworkBehaviour, IDamageAble
{
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private EntityAuthority _authority;
    [Inject] private ItemAnimation _animation;
    [Inject] private ExperienceModule _experience;

    public override void OnNetworkSpawn()
    {
        _authority.SetNetworkType(IsOwner);
        Initialize();
    }

    public void SetPlayer()
    {
        _authority.SetNetworkType(_playerRegistry.LocalPlayer == null);
        Initialize();
    }

    private void Initialize()
    {
        Model.Initialize(this);
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        _animation.SetConfig(_animationSettings);
        _playerRegistry.AddPlayer(this);
        _experience.Initialize(Model);
    }

    public float TakeDamage(HitInfo hit) => Model.TakeDamage(hit);
    public void SetDamageDealt(float damage) => Model.Attack.SetDamageDealt(damage);
}
}