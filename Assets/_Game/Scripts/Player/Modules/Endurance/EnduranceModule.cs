using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;
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

    public void Regenerate()
    {
        var endurance = Endurance;
        Endurance += EnduranceRecovery;
        if (Endurance > MaxEndurance)
        {
            Endurance = MaxEndurance;
        }
        if (Mathf.Approximately(endurance, Endurance)) return;
        OnEnduranceChanged?.Invoke(Endurance, Endurance);
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
        MaxEndurance = value;
        OnEnduranceChanged?.Invoke(Endurance, MaxEndurance);
    }

    private void UpdateEnduranceRecovery(float value)
    {
        EnduranceRecovery = value;
    }
}
}