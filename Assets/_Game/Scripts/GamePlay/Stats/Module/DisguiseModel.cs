namespace _Game.Scripts.GamePlay.Module
{
public class DisguiseModule: StatModule
{
    public float Disguise => _isMoving ? _disguise : _disguise + _disguiseInRest;
    
    private float _disguiseInRest;
    private float _disguise;

    private bool _isMoving;

    protected override void Configure()
    {
        BindStat(StatType.Disguise, UpdateDisguise);
        BindStat(StatType.DisguiseInRest, UpdateDisguiseInRest);
    }

    public void SetMoving(bool value)
    {
        _isMoving = value;
    }

    private void UpdateDisguise(float value) => _disguise = value;
    private void UpdateDisguiseInRest(float value) => _disguiseInRest = value;
}
}