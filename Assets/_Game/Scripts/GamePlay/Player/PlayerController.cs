using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [Header("Config")]
    [SerializeField] private PlayerConfig _playerConfig;
    [Header("Modules")]
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerHealth Health { get; private set; }
    [field: SerializeField] public PlayerVision Vision { get; private set; }
    [field: SerializeField] public PlayerMouth Mouth { get; private set; }
    [field: SerializeField] public PlayerEndurance Endurance { get; private set; }
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    public PlayerModel Model { get; private set; }

    [Inject] private Ticker _ticker;

    public void Initialize()
    {
        CreateModel(_ticker);
        InitializeActiveModules();
    }

    private void CreateModel(Ticker ticker)
    {
        Model = new(_playerConfig);
        
        var abilityFactory = new AbilityFactory(Model, ticker);
        Model.Abilities.SetFactory(abilityFactory);
        
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
    }

    private void InitializeActiveModules()
    {
        Vision.Construct(Model.Vision);
        Movement.Construct(Model.Movement);
        Mouth.Construct(Model.EatModule);
        Health.Construct(Model.Health);
        Endurance.Construct(Model.Endurance);
    }

    public void TakeDamage(float value, IDamageAble damager)
    {
        var amount = Model.Defense.ApplyResistance(value);
        
        Model.Health.TakeDamage(amount);
        Model.Defense.ReflectDamage(amount, damager);
    }

    private void OnDestroy()
    {
        Model.Dispose();
    }
}
}