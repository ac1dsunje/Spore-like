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

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [Header("Config")]
    [SerializeField] private PlayerConfig _playerConfig;
    [Header("Modules")]
    [SerializeField] private PlayerVision _vision;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerMouth _mouth;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerEndurance _endurance;
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    public PlayerModel Model { get; private set; }

    public void Initialize(Ticker ticker)
    {
        CreateModel(ticker);
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
        _vision.Construct(Model.Vision);
        _movement.Construct(Model.Movement);
        _mouth.Construct(Model.EatModule);
        _health.Construct(Model.Health);
        _endurance.Construct(Model.Endurance);
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