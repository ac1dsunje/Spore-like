using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Disguise
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