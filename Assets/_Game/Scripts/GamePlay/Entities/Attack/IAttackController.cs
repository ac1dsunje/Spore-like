using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Attack
{
public interface IAttackController
{
    public void RequestAttack(IDamageReceiver damageReceiver, Vector2 mousePosition);
}
}