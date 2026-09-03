using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class MouthModule: StatModule
{
    public float EatingStrength { get; private set; }
    public float EatingTime { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.EatingStrength, UpdateEatingStrength);
        BindStat(StatType.EatingTime, UpdateEatingTime);
    }

    private void UpdateEatingStrength(float value) => EatingStrength = value;
    private void UpdateEatingTime(float value) => EatingTime = value / 100f;
}
}