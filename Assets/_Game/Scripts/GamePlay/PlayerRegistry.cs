using System;
using _Game.Scripts.GamePlay.Entities;

namespace _Game.Scripts.GamePlay
{
public class PlayerRegistry
{
    public event Action<EntityController> OnPlayerInitialized;

    public void AddPlayer(EntityController player)
    {
        OnPlayerInitialized?.Invoke(player);
    }
}
}