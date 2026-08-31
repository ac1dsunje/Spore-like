using System;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Abilities
{
public enum AbilityActivationType
{
    Pressing,
    Toggle,
}
public abstract class Ability: IDisposable, IEnduranceUser
{
    private readonly AbilityConfig _config;

    private readonly EnduranceModule _endurance;

    private readonly Ticker _ticker;
    
    private bool _isActive;
    
    protected Ability(EnduranceModule endurance, AbilityConfig config, Ticker ticker)
    {
        _config = config;
        _endurance = endurance;
        _ticker = ticker;
        _ticker.OnTick += Update;
    }
    
    private void Update(float deltaTime)
    {
        switch (_config.ActivationType)
        {
            case AbilityActivationType.Pressing:
                UpdatePressing();
                break;

            case AbilityActivationType.Toggle:
                UpdateToggle();
                break;
        }

        if (!_isActive)
            return;

        if (_config.HasActivePhase)
        {
            if (_endurance.HasEnoughEndurance(_config.InUseCost * deltaTime))
                Do(deltaTime);
            else
                Disable();
        }
    }

    private void UpdatePressing()
    {
        if (Input.GetKey(_config.Key) &&
            !_isActive &&
            _endurance.HasEnoughEndurance(_config.StartCost))
        {
            Enable();
        }

        if (Input.GetKeyUp(_config.Key) && _isActive)
        {
            Disable();
        }
    }

    private void UpdateToggle()
    {
        if (!Input.GetKeyDown(_config.Key))
            return;

        if (_isActive)
            Disable();
        else if (_endurance.HasEnoughEndurance(_config.StartCost))
            Enable();
    }
    
    protected virtual void Enable()
    {
        _isActive = true;
        _endurance.AddUser(this);
        _endurance.UseEndurance(_config.StartCost);
    }

    protected virtual void Do(float deltaTime)
    {
        if (!_config.HasActivePhase) return;
        _endurance.UseEndurance(_config.InUseCost * deltaTime);
    }

    protected virtual void Disable()
    {
        _isActive = false;
        _endurance.RemoveUser(this);
    }

    public void Dispose()
    {
        _ticker.OnTick -= Update;
        if (_isActive) Disable();
    }
}
}