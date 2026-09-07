using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityEndurance: ITickable
{
    [Inject] private EnduranceModule _module;

    public void Tick()
    {
        if (!_module.IsUsed)
        {
            _module.AddEndurance(_module.EnduranceRecovery * Time.deltaTime);
        }
    }
}
}