using System;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay
{
public class PlayerRegistry
{
    public event Action<PlayerController> OnPlayerInitialized;

    public void AddPlayer(PlayerController player)
    {
        OnPlayerInitialized?.Invoke(player);
    }
}
}