using System;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules.Endurance;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityActivationType
{
    Pressing = 0,
    Toggle = 1,
}

public abstract class Ability : IDisposable, IEnduranceUser
{
    public AbilityConfig Config { get; }

    public bool IsActive { get; private set; }

    private readonly EnduranceModule _endurance;
    private readonly EnduranceRecoveryModule _recovery;
    
    protected Ability(EnduranceModule endurance, EnduranceRecoveryModule recovery, AbilityConfig config)
    {
        Config = config;
        _endurance = endurance;
        _recovery = recovery;
    }
    
    public void Update(float deltaTime)
    {
        if (!IsActive)
            return;

        if (!Config.HasActivePhase) return;
        if (_endurance.HasEnoughEndurance(Config.InUseCost * deltaTime))
            Do(deltaTime);
        else
            Disable();
    }

    public void TryActivate()
    {
        if (IsActive) return;
        if (!_endurance.HasEnoughEndurance(Config.StartCost)) return;
        
        Enable();
    }

    public void TryDeactivate()
    {
        if (!IsActive) return;
        
        Disable();
    }
    
    protected virtual void Enable()
    {
        IsActive = true;
        _recovery.AddUser(this);
        _endurance.UseEndurance(Config.StartCost);
    }

    protected virtual void Do(float deltaTime)
    {
        if (!Config.HasActivePhase) return;
        _endurance.UseEndurance(Config.InUseCost * deltaTime);
    }

    protected virtual void Disable()
    {
        IsActive = false;
        _recovery.RemoveUser(this);
    }

    public void Dispose()
    {
        if (IsActive) Disable();
    }
}
}