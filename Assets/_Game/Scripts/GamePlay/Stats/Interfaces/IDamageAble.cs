namespace _Game.Scripts.GamePlay.Interfaces
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
    public void TakeDamage(HitInfo hit);
}
}