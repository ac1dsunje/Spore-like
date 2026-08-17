using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Module;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class LightAbility: Ability
{
    private readonly VisionModule _vision;

    public LightAbility(VisionModule vision, EnduranceModule endurance, AbilityConfig config, 
        Ticker ticker, IInputService input) : base(endurance, config, ticker, input)
    {
        _vision = vision;
    }

    protected override void Enable()
    {
        base.Enable();
        _vision.SetLight(true);
    }

    protected override void Disable()
    {
        base.Disable();
        _vision.SetLight(false);
    }
}
}