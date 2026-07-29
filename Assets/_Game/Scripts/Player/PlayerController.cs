using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    private PlayerStats _stats;

    public void Construct(PlayerStats stats)
    {
        _stats = stats;
    }

    public float TakeDamage(float amount)
    {
        _stats.Health.TakeDamage(amount);
        return _stats.Attack.ReflectDamage(amount);
    }

    private void OnDestroy()
    {
        _stats.Dispose();
    }
}
}