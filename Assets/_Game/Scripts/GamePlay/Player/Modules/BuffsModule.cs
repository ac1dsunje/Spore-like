using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Buffs.Types;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public class BuffsModule
{
    public event Action<Buff> OnBuffActivated;
    public event Action<Buff> OnBuffDeactivated;
    
    private readonly List<Buff> _buffs = new();

    private PlayerModel _player;
    
    [Inject] private BuffsDatabase _dataDatabase;
    [Inject] private Ticker _ticker;
    
    public void Initialize()
    {
        foreach (var buff in _dataDatabase.Buffs)
        {
            switch (buff.Type)
            {
                case BuffType.Suffocating:
                {
                    _buffs.Add(new SuffocatingDebuff(_player.Stats, _player.Health, _ticker, buff));
                    break;
                }
                case BuffType.BadPassAbility:
                {
                    _buffs.Add(new BadPassAbility(_player.Stats, _ticker, buff));
                    break;
                }
                case BuffType.Heat:
                {
                    _buffs.Add(new HeatDebuff(_player.Stats, _player.Health, _ticker, buff));
                    break;
                }
                case BuffType.Cold:
                {
                    _buffs.Add(new ColdDebuff(_player.Stats, _player.Health, _ticker, buff));
                    break;
                }
                default:
                    Debug.Log($"Buff with type {buff.Type} is not implemented");
                    break;
            }
        }
    }

    public void SetModel(PlayerModel player)
    {
        _player = player;
    }

    public void Set(BuffType type, bool state)
    {
        Buff currentBuff = null;
        
        foreach (var buff in _buffs)
        {
            if (buff.Type != type) continue;
            currentBuff = buff;
            break;
        }

        if (currentBuff == null)
        {
            Debug.Log($"buff with type {type} not found in database");
            return;
        }

        if (state)
        {
            currentBuff.Activate();
            OnBuffActivated?.Invoke(currentBuff);
        }
        else
        {
            currentBuff.Deactivate();
            OnBuffDeactivated?.Invoke(currentBuff);
        }
    }
}
}