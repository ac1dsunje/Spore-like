using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay
{
public class PlayerRegistry
{
    public event Action<PlayerController> OnPlayerAdded;
    public event Action<PlayerController> OnLocalPlayerAdded;
    public event Action<PlayerController> OnPlayerRemoved;
    
    private readonly HashSet<PlayerController> _players = new();

    public void AddPlayer(PlayerController player)
    {
        if (!_players.Add(player)) return;
        OnPlayerAdded?.Invoke(player);

        if (player.IsLocalPlayer)
        {
            OnLocalPlayerAdded?.Invoke(player);
        }
    }

    public void RemovePlayer(PlayerController player)
    {
        _players.Remove(player);
        OnPlayerRemoved?.Invoke(player);
    }
}
}