using _Game.Scripts.GamePlay.Modules.Endurance;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityEndurance : ITickable
{
    private readonly EnduranceModule _endurance;
    private readonly EnduranceRecoveryModule _recovery;

    public EntityEndurance(EnduranceModule endurance, EnduranceRecoveryModule recovery)
    {
        _endurance = endurance;
        _recovery = recovery;
    }

    public void Tick()
    {
        if (!_recovery.IsRecovering || _recovery.RecoveryRate <= 0f) return;
            
        _endurance.Add(_recovery.RecoveryRate * Time.deltaTime);
    }
}
}