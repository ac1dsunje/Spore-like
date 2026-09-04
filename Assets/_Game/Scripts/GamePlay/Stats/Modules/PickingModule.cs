using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class PickingModule: StatModule
{
    public float PickingRange { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.PickingRange, UpdatePickingRange);
    }

    private void UpdatePickingRange(float value) => PickingRange = value;
}
}