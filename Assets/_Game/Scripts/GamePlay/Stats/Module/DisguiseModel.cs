namespace _Game.Scripts.GamePlay.Module
{
public class DisguiseModule: StatModule
{
    public float Disguise { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.Disguise, UpdateDisguise);
    }

    private void UpdateDisguise(float value) => Disguise = value;
}
}