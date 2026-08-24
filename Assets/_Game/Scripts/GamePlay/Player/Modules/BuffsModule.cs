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
    private readonly List<Buff> _buffs = new();
    
    [Inject]
    private void Construct(BuffsDatabase database, EntityStats entityStats, PlayerModel playerModel, Ticker ticker)
    {
        
        foreach (var buff in database.Buffs)
        {
            if (buff.Type == BuffType.Suffocating)
            {
                var newBuff = new SuffocatingDebuff(entityStats, playerModel.Health, ticker, buff);
                _buffs.Add(newBuff);
            }
            else
            {
                Debug.Log($"Buff with type {buff.Type} is not implemented");
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
        }
        else
        {
            currentBuff.Deactivate();
        }
    }
}
}