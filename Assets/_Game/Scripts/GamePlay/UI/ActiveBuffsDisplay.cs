using System.Collections.Generic;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveBuffsDisplay: MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform  _container;
    
    private BuffsModule _player;

    private readonly Dictionary<Buff, ActiveBuffSlotUI> _slots = new();

    public void Construct(BuffsModule player)
    {
        _player = player;
        _player.OnBuffActivated += AddBuff;
        _player.OnBuffDeactivated += RemoveBuff;
    }

    private void AddBuff(Buff buff)
    {
        var slot = Instantiate(_slotPrefab, _container).GetComponent<ActiveBuffSlotUI>();
        slot.Construct(buff);
        _slots[buff] = slot;
    }

    private void RemoveBuff(Buff buff)
    {
        if (!_slots.TryGetValue(buff, out var slot)) return;
        Destroy(slot.gameObject);
        _slots.Remove(buff);
    }

    private void OnDestroy()
    {
        _player.OnBuffActivated -= AddBuff;
        _player.OnBuffDeactivated -= RemoveBuff;
    }
}
}