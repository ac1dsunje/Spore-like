namespace _Game.Scripts.GamePlay.Player.Modules
{
public interface IDamageAble
{
    public void TakeDamage(float amount, IDamageAble damager);
}
}