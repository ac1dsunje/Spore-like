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
        if (!_use) return;
        
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
}
}