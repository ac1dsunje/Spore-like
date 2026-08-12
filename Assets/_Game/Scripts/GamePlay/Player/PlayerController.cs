using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Attack;
using _Game.Scripts.GamePlay.Player.Modules.BiomeChecker;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using _Game.Scripts.GamePlay.Rarities;
using _Game.Scripts.GamePlay.World;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [Inject] private PlayerConfig _playerConfig;
    [Header("Modules")]
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerHealth Health { get; private set; }
    [field: SerializeField] public PlayerVision Vision { get; private set; }
    [field: SerializeField] public PlayerMouth Mouth { get; private set; }
    [field: SerializeField] public PlayerEndurance Endurance { get; private set; }
    [field: SerializeField] public PlayerBiome BiomeChecker { get; private set; }
    [field: SerializeField] public PlayerAttack Attack { get; private set; }
    [field: SerializeField] public ItemAnimation Animation { get; private set; }
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    [Inject] public PlayerModel Model { get; private set; }

    [Inject] private Ticker _ticker;
    [Inject] private WorldModel _worldModel;
    [Inject] private PlayerInputService _playerInput;
    [Inject] private AbilityFactory _abilityFactory;

    public void Initialize()
    {
        CreateModel();
        InitializeActiveModules();
    }

    private void CreateModel()
    {
        Model.Abilities.Initialize(_abilityFactory, Model);
        
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
    }

    private void InitializeActiveModules()
    {
        Vision.Construct(Model.Vision);
        Movement.Construct(Model.Movement, _playerInput);
        Mouth.Construct(Model.MouthModule);
        Health.Construct(Model.Health);
        Endurance.Construct(Model.Endurance);
        BiomeChecker.Construct(_worldModel, Model);
        Attack.Construct(Model.Attack, _playerInput, this);
        Animation.SetConfig(_playerConfig.AnimationConfig);
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = Model.Defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        Model.Health.TakeDamage(damage);
        var returnedDamage = Model.Defense.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, Model.Attack.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
    }

    private void OnDestroy()
    {
        Model.Dispose();
    }
}
}