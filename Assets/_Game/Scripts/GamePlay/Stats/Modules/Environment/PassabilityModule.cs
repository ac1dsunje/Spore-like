using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules.Environment
{
public class PassabilityModule : StatModule
{
    private float _passAbility;

    protected override void Configure()
    {
        BindStat(StatType.Passability, UpdatePassability);
    }
    
    public bool IsBadPassAbility(float biomePassAbility) => biomePassAbility > _passAbility;
    
    private void UpdatePassability(float value) => _passAbility = value;
}
}