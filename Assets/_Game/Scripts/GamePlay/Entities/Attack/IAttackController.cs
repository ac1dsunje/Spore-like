using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public interface IAttackController
{
    public void RequestAttack(Vector2 mousePosition);
}
}