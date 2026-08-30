using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Buffs.Types;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules
{
public class BuffsModule
{
    public event Action<Buff> OnBuffActivated;
    public event Action<Buff> OnBuffDeactivated;
    
    private readonly List<Buff> _buffs = new();

    [Inject] private EntityStats _stats;
    [Inject] private HealthModule _health;
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
                    _buffs.Add(new SuffocatingDebuff(_stats, _health, _ticker, buff));
                    break;
                }
                case BuffType.BadPassAbility:
                {
                    _buffs.Add(new BadPassAbility(_stats, _ticker, buff));
                    break;
                }
                case BuffType.Heat:
                {
                    _buffs.Add(new HeatDebuff(_stats, _health, _ticker, buff));
                    break;
                }
                case BuffType.Cold:
                {
                    _buffs.Add(new ColdDebuff(_stats, _health, _ticker, buff));
                    break;
                }
                case BuffType.Starvation:
                    _buffs.Add(new StarvationDebuff(_stats, _health, _ticker, buff));
                    break;
                case BuffType.Overeating:
                    _buffs.Add(new OvereatingDebuff(_stats, _health, _ticker, buff));
                    break;
                default:
                    Debug.Log($"Buff with type {buff.Type} is not implemented");
                    break;
            }
        }
    }

    public void Set(BuffType type, bool state)
    {
        var currentBuff = _buffs.FirstOrDefault(buff => buff.Type == type);

        if (currentBuff == null)
        {
            Debug.Log($"buff with type {type} not found in database");
            return;
        }

        if (state && !currentBuff.IsActive)
        {
            currentBuff.Activate();
            OnBuffActivated?.Invoke(currentBuff);
        }
        else if (!state && currentBuff.IsActive)
        {
            currentBuff.Deactivate();
            OnBuffDeactivated?.Invoke(currentBuff);
        }
    }
}
}