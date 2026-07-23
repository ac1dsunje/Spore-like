using _Game.Scripts.Player.Modules.Movement;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    public PlayerStats Stats { get; private set; }
    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    public void Construct(PlayerStats stats)
    {
        Stats = stats;
    }

    public float TakeDamage(float amount)
    {
        Stats.Health.TakeDamage(amount);
        return Stats.Attack.ReflectDamage(amount);
    }

    public void Disable() => _movement.Disable();

    public void Enable() => _movement.Enable();

    private void OnDestroy()
    {
        Stats.Dispose();
    }
}
}