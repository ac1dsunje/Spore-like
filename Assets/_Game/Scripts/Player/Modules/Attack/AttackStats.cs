namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackStats
{
    public float DamageReflection { get; private set; }
    public float PhysicalDamage { get; private set; }
    
    public float ReflectDamage(float damage)
    {
        return damage * DamageReflection / 100;
    }

    public void UpdateDamageReflection(float newValue)
    {
        DamageReflection = newValue;
    }

    public void UpdatePhysicalDamage(float newValue)
    {
        PhysicalDamage = newValue;
    }
}
}