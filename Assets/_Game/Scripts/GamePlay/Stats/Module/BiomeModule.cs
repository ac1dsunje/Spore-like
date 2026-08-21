using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Module
{
public class BiomeModule: StatModule
{
    public float PassAbility { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.Passability, UpdatePassability);
    }

    private void UpdatePassability(float value) => PassAbility = value;
}
}