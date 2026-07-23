using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float DamageReflection { get; private set; }
    public float PhysicalDamage { get; private set; }

    private PlayerStats _stats;

    public AttackModule(PlayerStats playerStats): base(playerStats) {}

    protected override void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.DamageReflection:
                UpdateDamageReflection(value);
                break;
            case EvolutionType.PhysicalDamage:
                UpdatePhysicalDamage(value);
                break;
        }
    }
    
    public float ReflectDamage(float damage)
    {
        return damage * DamageReflection / 100;
    }

    private void UpdateDamageReflection(float newValue) => DamageReflection = newValue;

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;
}
}