using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }

    public AttackModule(PlayerStats playerStats): base(playerStats) {}

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.PhysicalDamage:
                UpdatePhysicalDamage(value);
                break;
        }
    }

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;
}
}