using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class EnvironmentModule : StatModule
{
    private float _passAbility;
    private float _minimalComfortable;
    private float _maximumComfortable;
    private float _coldResistance;
    private float _heatResistance;

    protected override void Configure()
    {
        BindStat(StatType.Passability, UpdatePassability);
        BindStat(StatType.MinimalComfortableTemperature, UpdateMinimalComfortable);
        BindStat(StatType.MaximumComfortableTemperature, UpdateMaximumComfortable);
        BindStat(StatType.ColdResistance, UpdateColdResistance);
        BindStat(StatType.HeatResistance, UpdateHeatResistance);
    }
    
    public bool IsBadPassAbility(float biomePassAbility) => biomePassAbility > _passAbility;
    public bool IsCold(float biomeTemperature) => biomeTemperature < _minimalComfortable - _coldResistance;
    public bool IsHot(float biomeTemperature) => biomeTemperature > _maximumComfortable + _heatResistance;
    
    private void UpdatePassability(float value) => _passAbility = value;
    private void UpdateMinimalComfortable(float value) => _minimalComfortable = value;
    private void UpdateMaximumComfortable(float value) => _maximumComfortable = value;
    private void UpdateColdResistance(float value) => _coldResistance = value;
    private void UpdateHeatResistance(float value) => _heatResistance = value;
}
}