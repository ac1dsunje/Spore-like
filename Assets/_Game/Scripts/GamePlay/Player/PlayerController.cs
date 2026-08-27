using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Behaviours;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : MonoBehaviour
{
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private ItemAnimation _animation;
    [Inject] private ExperienceModule _experience;
    [Inject] private PlayerAttack _attack;
    [Inject] private PlayerHealth _health;

    public void Initialize()
    {
        _attack.SetReceiver(_health);
        _health.SetAttackSource(_attack);
        
        Model.Initialize();
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        Model.Buffs.Initialize();
        _animation.SetConfig(_animationSettings);
        _playerRegistry.AddPlayer(this);
        _experience.Initialize(Model);
    }
}
}