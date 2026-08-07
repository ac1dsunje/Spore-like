using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions;
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
    [SerializeField] private PlayerVision _playerVision;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerMouth _playerMouth;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerEndurance _playerEndurance;
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    public PlayerModel Model { get; private set; }

    public void Initialize(Ticker ticker)
    {
        Model = new(_playerConfig);
        
        var abilityFactory = new AbilityFactory(Model, ticker);
        Model.Abilities.SetFactory(abilityFactory);
        
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        
        _playerVision.Construct(Model.Vision);
        _playerMovement.Construct(Model.Movement);
        _playerMouth.Construct(Model.EatModule);
        _playerHealth.Construct(Model.Health);
        _playerEndurance.Construct(Model.Endurance);
    }

    public void TakeDamage(float value, IDamageAble damager)
    {
        var amount = Model.Defense.GetDamageAfterResistance(value);
        
        Model.Health.TakeDamage(amount);
        Model.Defense.ReflectDamage(amount, damager);
    }

    private void OnDestroy()
    {
        Model.Dispose();
    }
}
}