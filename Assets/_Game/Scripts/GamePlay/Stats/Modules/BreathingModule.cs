using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class BreathingModule: StatModule
{
    public float OxygenBreathing { get; private set; }
    public float HydrogenBreathing { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.OxygenBreathing, UpdateOxygen);
        BindStat(StatType.HydrogenBreathing, UpdateHydrogen);
    }

    private void UpdateOxygen(float value) => OxygenBreathing = value;
    private void UpdateHydrogen(float value) => HydrogenBreathing = value;
}
}