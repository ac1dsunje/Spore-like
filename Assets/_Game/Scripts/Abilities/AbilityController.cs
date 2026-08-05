using _Game.Scripts.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
public abstract class AbilityController: MonoBehaviour
{
    [SerializeField] protected AbilityConfig Config;
    [SerializeField] private bool _use;
    
    protected EnduranceModule Endurance;
    
    protected void Construct(EnduranceModule module)
    {
        Endurance = module;
    }
}
}