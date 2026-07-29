namespace _Game.Scripts.Player
{
public interface IDamageAble
{
    public void TakeDamage(float amount, IDamageAble damager);
}
}