using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [SerializeField] private PlayerVision _playerVision;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerMouth _playerMouth;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerEndurance _playerEndurance;

    [SerializeField] private PlayerConfig _playerConfig;
    
    public PlayerModel Model { get; private set; }

    public void Initialize(Ticker ticker)
    {
        Model = new(_playerConfig);
        
        var abilityFactory = new AbilityFactory(Model, ticker);
        Model.Abilities.SetFactory(abilityFactory);
        
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