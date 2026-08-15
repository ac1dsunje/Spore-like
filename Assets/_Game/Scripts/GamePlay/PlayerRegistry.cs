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

    public PlayerController LocalPlayer {get; private set;}

    public void AddPlayer(PlayerController player)
    {
        if (!_players.Add(player)) return;
        OnPlayerAdded?.Invoke(player);

        if (player.IsLocalPlayer)
        {
            LocalPlayer = player;
            OnLocalPlayerAdded?.Invoke(LocalPlayer);
        }
    }

    public void RemovePlayer(PlayerController player)
    {
        _players.Remove(player);
        OnPlayerRemoved?.Invoke(player);
    }
}
}