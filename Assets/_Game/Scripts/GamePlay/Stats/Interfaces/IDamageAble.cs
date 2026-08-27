namespace _Game.Scripts.GamePlay.Interfaces
{
public struct HitInfo
{
    public float Damage { get; private set; }
    public float IgnoreResistance { get; private set; }
    public IDamageAble Owner { get; private set; }

    public HitInfo(float damage, float ignoreResistance, IDamageAble owner)
    {
        Damage = damage;
        IgnoreResistance = ignoreResistance;
        Owner = owner;
    }
}

public interface IDamageAble
{
    public float TakeDamage(HitInfo hit);
    public void SetDamageDealt(float damage);
}
}