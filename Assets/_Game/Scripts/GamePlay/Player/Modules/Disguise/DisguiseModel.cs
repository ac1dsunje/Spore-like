using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Disguise
{
public class DisguiseModule: StatModule
{
    public float Disguise { get; private set; }

    [Inject]
    public DisguiseModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.Disguise, UpdateDisguise);
    }

    private void UpdateDisguise(float value) => Disguise = value;
}
}