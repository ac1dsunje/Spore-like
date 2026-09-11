using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules.Health
{
public class RegenerationModule : StatModule
{
    public float Current { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.Regeneration, UpdateRegeneration);
    }

    private void UpdateRegeneration(float value) => Current = value;
}
}