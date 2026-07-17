using _Game.Scripts.Buffs;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.UI
{
public class BuffChooseScreen: ScreenManager
{
    [SerializeField] private BuffSlotUI[] _slots;
    private PlayerController _player;
    
    public void Construct(PlayerController player)
    {
        _player = player;
        _player.OnLevelChanged += UpdateLevel;
    }

    private void Start()
    {
        HideScreen();
    }

    private void UpdateLevel(int level)
    {
        ShowScreen();
        for (var i = 0; i < _slots.Length; i++)
        {
            //ToDo: choose buffs to set for slots (actually not here, in other business-logic script)
            _slots[i].SetBuff(i + 1);
        }
    }

    private void OnDestroy()
    {
        _player.OnLevelChanged -= UpdateLevel;
    }
}
}