using System;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay
{
public class PlayerRegistry
{
    public event Action<PlayerController> OnPlayerAdded;
    public event Action<PlayerController> OnPlayerRemoved;

    public void NotifyPlayerAdded(PlayerController player)
    {
        OnPlayerAdded?.Invoke(player);
    }

    public void NotifyPlayerRemoved(PlayerController player)
    {
        OnPlayerRemoved?.Invoke(player);
    }
}
}