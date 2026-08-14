using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerEndurance: PlayerNetworkBehaviour
{
    private EnduranceModule _module;
    
    [Inject]
    private void Construct(EnduranceModule module)
    {
        _module = module;
    }

    private void Update()
    {
        if (!_module.IsUsed && IsLocal)
        {
            _module.AddEndurance(_module.EnduranceRecovery * Time.deltaTime);
        }
    }
}
}