using _Game.Scripts.Player.Modules;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.World.Food;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IEater, IDamageAble
{
    public PlayerStats Stats { get; private set; }
    private PlayerMovement _playerMovement;
    
    public void Construct(PlayerStats stats, PlayerMovement playerMovement)
    {
        Stats = stats;
        _playerMovement = playerMovement;
    }
    
    public void Eat(int amount, FoodItem food)
    {
        Stats.Eat(amount, food);
    }

    public float TakeDamage(float amount)
    {
        return Stats.TakeDamage(amount);
    }

    public void Disable() => _playerMovement.Disable();

    public void Enable() => _playerMovement.Enable();
}
}