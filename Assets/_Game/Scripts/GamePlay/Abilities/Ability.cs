using System;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Abilities
{
public abstract class Ability: IDisposable
{
    private readonly AbilityConfig _config;

    protected PlayerModel Model;
    private readonly EnduranceModule _endurance;

    private readonly Ticker _ticker;
    
    private bool _isActive;
    
    protected Ability(PlayerModel model, AbilityConfig config, Ticker ticker)
    {
        _config = config;
        Model = model;
        _endurance = model.Endurance;
        _ticker = ticker;
        _ticker.OnTick += Update;
    }
    
    private void Update(float deltaTime)
    { 
        if (Input.GetKeyDown(_config.Key) && _endurance.HasEnoughEndurance(_config.StartCost) && !_isActive)
        {
            Enable();
        }

        if (Input.GetKeyUp(_config.Key) && _isActive)
        {
            Disable();
            return;
        }

        if (!_isActive) return;

        if (_endurance.HasEnoughEndurance(_config.InUseCost * Time.deltaTime))
            Do();
        else
            Disable();
    }
    
    protected virtual void Enable()
    {
        _isActive = true;
        _endurance.AddUser(this);
        _endurance.UseEndurance(_config.StartCost);
    }

    protected virtual void Do()
    {
        if (!_config.HasActivePhase) return;
        _endurance.UseEndurance(_config.InUseCost * Time.deltaTime);
    }

    protected virtual void Disable()
    {
        _isActive = false;
        _endurance.RemoveUser(this);
    }

    public void Dispose()
    {
        _ticker.OnTick -= Update;
    }
}
}