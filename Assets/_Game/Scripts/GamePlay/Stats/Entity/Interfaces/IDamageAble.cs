namespace _Game.Scripts.GamePlay.Entity.Interfaces
{
public struct HitInfo
{
    public float Damage;
    public float IgnoreResistance;
    public IDamageAble Owner;

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