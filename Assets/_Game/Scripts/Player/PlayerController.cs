using _Game.Scripts.Player.Modules.Movement;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    public PlayerStats Stats { get; private set; }
    private PlayerMovement _movement;
    
    public void Construct(PlayerStats stats, PlayerMovement playerMovement)
    {
        Stats = stats;
        _movement = playerMovement;
    }

    public float TakeDamage(float amount)
    {
        return Stats.TakeDamage(amount);
    }

    public void Disable() => _movement.Disable();

    public void Enable() => _movement.Enable();

    private void OnDestroy()
    {
        Stats.Dispose();
    }
}
}