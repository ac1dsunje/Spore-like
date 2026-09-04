using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class PickingModule: StatModule
{
    public float PickingRange { get; private set; }

    public event Action<float> OnExperiencePointCollected;

    protected override void Configure()
    {
        BindStat(StatType.PickingRange, UpdatePickingRange);
    }

    public void GetExperiencePoint(float amount)
    {
        OnExperiencePointCollected?.Invoke(amount);
    }

    private void UpdatePickingRange(float value) => PickingRange = value;
}
}