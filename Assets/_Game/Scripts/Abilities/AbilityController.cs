using _Game.Scripts.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
public abstract class AbilityController: MonoBehaviour
{
    [SerializeField] private bool _use;
    
    protected EnduranceModule _endurance;
    
    public void Construct(EnduranceModule module)
    {
        _endurance = module;
    }
}
}