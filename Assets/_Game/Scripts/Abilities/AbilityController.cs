using _Game.Scripts.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
public abstract class AbilityController: MonoBehaviour
{
    [SerializeField] private AbilityConfig _config;
    [SerializeField] private bool _use;

    private bool _isActive;

    private EnduranceModule _endurance;
    
    protected void Construct(EnduranceModule module)
    {
        _endurance = module;
    }
    
    private void Update()
    {
        var cost = _config.InUseCost * Time.deltaTime;
        var isAble = _endurance.HasEnoughEndurance(cost);
        
        if (Input.GetKeyDown(_config.Key) && _endurance.HasEnoughEndurance(_config.StartCost) && !_isActive)
        {
            StartUsing();
        }

        if (Input.GetKeyUp(_config.Key))
        {
            Stop();
            return;
        }

        if (!_isActive) return;

        if (isAble)
            Do();
        else
            Stop();
    }
    
    protected virtual void StartUsing()
    {
        _isActive = true;
        _endurance.AddUser(this);
        _endurance.UseEndurance(_config.StartCost);
    }

    protected virtual void Do()
    {
        _endurance.UseEndurance(_config.InUseCost * Time.deltaTime);
    }

    protected virtual void Stop()
    {
        _isActive = false;
        _endurance.RemoveUser(this);
    }
}
}