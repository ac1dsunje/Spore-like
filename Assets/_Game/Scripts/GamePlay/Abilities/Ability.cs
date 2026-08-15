using System;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Abilities
{
public abstract class Ability: IDisposable, IEnduranceUser
{
    private readonly AbilityConfig _config;

    protected readonly PlayerModel Model;
    private readonly EnduranceModule _endurance;

    private readonly Ticker _ticker;
    private readonly IInputService _input;
    
    private bool _isActive;
    
    protected Ability(PlayerModel model, AbilityConfig config, Ticker ticker, IInputService inputService)
    {
        _input = inputService;
        _config = config;
        Model = model;
        _endurance = model.Endurance;
        _ticker = ticker;
        _ticker.OnTick += Update;
    }
    
    private void Update(float deltaTime)
    {
        if (_input.WasKeyPressed(_config.Key) && _endurance.HasEnoughEndurance(_config.StartCost) && !_isActive)
        {
            Enable();
        }

        if (_input.WasKeyReleased(_config.Key) && _isActive)
        {
            Disable();
            return;
        }

        if (!_isActive) return;

        if (_endurance.HasEnoughEndurance(_config.InUseCost * deltaTime))
            Do(deltaTime);
        else
            Disable();
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