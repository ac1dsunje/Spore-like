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

    public float TakeDamage(float amount)
    {
        _model.Health.TakeDamage(amount);
        return _model.Attack.ReflectDamage(amount);
    }

    private void OnDestroy()
    {
        _model.Dispose();
    }
}
}