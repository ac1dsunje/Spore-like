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

    public void TakeDamage(float value, IDamageAble damager)
    {
        var amount = _model.Defense.GetDamageAfterResistance(value);
        
        _model.Health.TakeDamage(amount);
        _model.Defense.ReflectDamage(amount, damager);
    }

    private void OnDestroy()
    {
        _model.Dispose();
    }
}
}