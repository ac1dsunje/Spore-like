using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class DisguiseModule: StatModule
{
    public float Disguise => _isMoving ? _disguise : _disguise + _disguiseInRest;
    
    private float _disguiseInRest;
    private float _disguise;

    private bool _isMoving;

    public event Action OnUnnoticed;
    public event Action OnUnnoticedInRest;

    protected override void Configure()
    {
        BindStat(StatType.Disguise, UpdateDisguise);
        BindStat(StatType.DisguiseInRest, UpdateDisguiseInRest);
    }

    public void SetMoving(bool value) => _isMoving = value;

    public bool TryNotice(float sensorics, bool xRay)
    {
        var notice = sensorics >= Disguise || xRay;

        if (!notice)
        {
            if (!_isMoving)
                OnUnnoticedInRest?.Invoke();
            
            OnUnnoticed?.Invoke();
        }

        
        
        return notice;
    }

    private void UpdateDisguise(float value) => _disguise = value;
    private void UpdateDisguiseInRest(float value) => _disguiseInRest = value;
}
}