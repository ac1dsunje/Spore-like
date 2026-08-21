using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class XRayAbility: Ability
{
    private readonly VisionModule _vision;

    public XRayAbility(VisionModule vision, EnduranceModule endurance, AbilityConfig config, 
        Ticker ticker, IInputService input) : base(endurance, config, ticker, input)
    {
        _vision = vision;
    }

    protected override void Enable()
    {
        base.Enable();
        _vision.SetXRay(true);
    }

    protected override void Disable()
    {
        base.Disable();
        _vision.SetXRay(false);
    }
}
}