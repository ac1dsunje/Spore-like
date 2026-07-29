using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    private PlayerModel _model;

    public void Construct(PlayerModel model)
    {
        _model = model;
    }

    public void TakeDamage(float amount, IDamageAble damager)
    {
        _model.Health.TakeDamage(amount);
        _model.Attack.ReflectDamage(amount, damager);
    }

    private void OnDestroy()
    {
        _model.Dispose();
    }
}
}