using System;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.Stats;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Endurance
{
public class EnduranceModule: StatModule
{
    public float MaxEndurance {get;  private set; }
    public float Endurance { get; private set; }
    public float EnduranceRecovery { get; private set; }
    
    public event Action<float, float> OnEnduranceChanged;

    public EnduranceModule(PlayerStats playerStats): base(playerStats) {}

    public void AddEndurance(float value)
    {
        var endurance = Endurance;
        Endurance += value;
        if (Endurance > MaxEndurance)
        {
            Endurance = MaxEndurance;
        }
        if (Mathf.Approximately(endurance, Endurance)) return;
        OnEnduranceChanged?.Invoke(Endurance, MaxEndurance);
    }

    public void UseEndurance(float value)
    {
        var endurance = Endurance;
        Endurance -= value;
        if (Endurance <= 0) Endurance = 0;
        
        if (Mathf.Approximately(endurance, Endurance)) return;
        OnEnduranceChanged?.Invoke(Endurance, MaxEndurance);
    }

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.MaxEndurance:
                UpdateMaxEndurance(value);
                break;
            case StatType.EnduranceRecovery:
                UpdateEnduranceRecovery(value);
                break;
        }
    }
    
    private void UpdateMaxEndurance(float value)
    {
        var difference = value - MaxEndurance;
        MaxEndurance = value;
        
        MaxEndurance = value;
    
        Endurance = Mathf.Clamp(Endurance + difference, 0, MaxEndurance);
        
        OnEnduranceChanged?.Invoke(Endurance, MaxEndurance);
    }

    private void UpdateEnduranceRecovery(float value)
    {
        EnduranceRecovery = value;
    }
}
}