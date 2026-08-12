using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Endurance
{
public class PlayerEndurance: MonoBehaviour
{
    private EnduranceModule _module;
    
    [Inject]
    private void Construct(EnduranceModule module)
    {
        _module = module;
    }

    private void Update()
    {
        if (!_module.IsUsed)
        {
            _module.AddEndurance(_module.EnduranceRecovery * Time.deltaTime);
        }
    }
}
}