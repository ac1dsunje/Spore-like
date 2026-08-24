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
    
    [Inject]
    private void Construct(BuffsDatabase database, EntityStats entityStats, PlayerModel playerModel, Ticker ticker)
    {
        
        foreach (var buff in database.Buffs)
        {
            switch (buff.Type)
            {
                case BuffType.Suffocating:
                {
                    _buffs.Add(new SuffocatingDebuff(entityStats, playerModel.Health, ticker, buff));
                    break;
                }
                case BuffType.BadPassAbility:
                {
                    _buffs.Add(new BadPassAbility(entityStats, playerModel.Movement, ticker, buff));
                    break;
                }
                case BuffType.Heat:
                case BuffType.Cold:
                default:
                    Debug.Log($"Buff with type {buff.Type} is not implemented");
                    break;
            }
        }
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