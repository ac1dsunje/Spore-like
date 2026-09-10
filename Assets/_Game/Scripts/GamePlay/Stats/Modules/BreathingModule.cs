using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class BreathingModule : StatModule
{
    private float _oxygenBreathing;
    private float _hydrogenBreathing;

    protected override void Configure()
    {
        BindStat(StatType.OxygenBreathing, UpdateOxygen);
        BindStat(StatType.HydrogenBreathing, UpdateHydrogen);
    }
    public bool IsSuffocating(float biomeOxygen, float biomeHydrogen)
    {
        var needsOxygen = _oxygenBreathing > 0f;
        var needsHydrogen = _hydrogenBreathing > 0f;

        if (!needsOxygen && !needsHydrogen)
            return false;

        var hasOxygen = needsOxygen && biomeOxygen >= _oxygenBreathing;
        var hasHydrogen = needsHydrogen && biomeHydrogen >= _hydrogenBreathing;

        return !hasOxygen && !hasHydrogen;
    }
    private void UpdateOxygen(float value) => _oxygenBreathing = value;
    private void UpdateHydrogen(float value) => _hydrogenBreathing = value;
}
}