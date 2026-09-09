namespace _Game.Scripts.GamePlay.Interfaces
{
public struct HitInfo
{
    public float Damage { get; private set; }
    public float IgnoreResistance { get; private set; }
    public IDamageSource Source { get; private set; }
    public IDamageReceiver Receiver { get; private set; }

    public HitInfo(float damage, float ignoreResistance, IDamageSource source, IDamageReceiver receiver)
    {
        Damage = damage;
        IgnoreResistance = ignoreResistance;
        Source = source;
        Receiver = receiver;
    }

    public void AddDamage(float damage)
    {
        Damage += damage;
    }

    public void AddIgnoreResistance(float ignoreResistance)
    {
        IgnoreResistance += ignoreResistance;
    }
}

public interface IDamageSource
{
    public void SetDamageDealt(float damage);
}

public interface IDamageReceiver
{
    public void TakeDamage(HitInfo hit);
}
}