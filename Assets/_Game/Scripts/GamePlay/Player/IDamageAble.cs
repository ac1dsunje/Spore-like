namespace _Game.Scripts.GamePlay.Player
{
public interface IDamageAble
{
    public void TakeDamage(float amount, IDamageAble damager);
}
}