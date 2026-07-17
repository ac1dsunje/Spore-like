using System.Collections.Generic;
using _Game.Scripts.Buffs;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class BuffChooseScreen : ScreenManager
{
    [SerializeField] private BuffSlotUI[] _slots;
    [SerializeField] private Buff[] _buffs;
    private PlayerController _player;
    
    public void Construct(PlayerController player)
    {
        _player = player;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked += BuffChoose;
        }
    }

    private void BuffChoose(Buff buff)
    {
        Debug.Log($"Buff {buff.Name} clicked");
        
        // TODO: applyBuff
        
        HideScreen();
        Time.timeScale = 1;
    }

    private void Start()
    {
        HideScreen();
    }
    
    private void UpdateLevel(int level)
    {
        ShowScreen();
        
        var availableBuffs = new List<Buff>(_buffs);
        
        var slotsToFill = Mathf.Min(_slots.Length, availableBuffs.Count);

        for (var i = 0; i < slotsToFill; i++)
        {
            var randomIndex = Random.Range(0, availableBuffs.Count);
            var chosenBuff = availableBuffs[randomIndex];
            
            _slots[i].SetBuff(chosenBuff); 
            
            availableBuffs.RemoveAt(randomIndex);
        }
        
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        _player.OnLevelChanged -= UpdateLevel;
        foreach (var slot in _slots)
        {
            slot.OnSlotClicked -= BuffChoose;
        }
    }
}
}