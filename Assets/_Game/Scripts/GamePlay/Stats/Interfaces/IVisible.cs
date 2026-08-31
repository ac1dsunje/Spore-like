namespace _Game.Scripts.GamePlay.Interfaces
{
public interface IVisible
{
    public bool IsDetected(float sensorics);
    public void SetVisible(bool state);
}
}