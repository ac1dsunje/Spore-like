using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class EnvironmentModule: StatModule
{
    private float _passAbility;
    private float _minimalComfortable;
    private float _maximumComfortable;
    private float _coldResistance;
    private float _heatResistance;
    private float _oxygenBreathing;
    private float _hydrogenBreathing;

    protected override void Configure()
    {
        BindStat(StatType.Passability, UpdatePassability);
        BindStat(StatType.OxygenBreathing, UpdateOxygen);
        BindStat(StatType.HydrogenBreathing, UpdateHydrogen);
        BindStat(StatType.MinimalComfortableTemperature, UpdateMinimalComfortable);
        BindStat(StatType.MaximumComfortableTemperature, UpdateMaximumComfortable);
        BindStat(StatType.ColdResistance, UpdateColdResistance);
        BindStat(StatType.HeatResistance, UpdateHeatResistance);
    }
    
    public bool IsBadPassAbility(float biomePassAbility) => biomePassAbility > _passAbility;
    public bool IsCold(float biomeTemperature) => biomeTemperature < _minimalComfortable - _coldResistance;
    public bool IsHot(float biomeTemperature) => biomeTemperature > _maximumComfortable + _heatResistance;
    public bool IsSuffocating(float biomeOxygen, float biomeHydrogen)
    {
        var needsOxygen = _oxygenBreathing > 0f;
        var needsHydrogen = _hydrogenBreathing > 0f;

        if (!needsOxygen && !needsHydrogen)
            return false;

        var hasOxygen = needsOxygen && biomeOxygen >= _oxygenBreathing;
        var hasHydrogen = needsHydrogen && biomeHydrogen >= _hydrogenBreathing;

        return !hasOxygen && !hasHydrogen;
    }
    
    private void UpdatePassability(float value) => _passAbility = value;
    private void UpdateOxygen(float value) => _oxygenBreathing = value;
    private void UpdateHydrogen(float value) => _hydrogenBreathing = value;
    private void UpdateMinimalComfortable(float value) => _minimalComfortable = value;
    private void UpdateMaximumComfortable(float value) => _maximumComfortable = value;
    private void UpdateColdResistance(float value) => _coldResistance = value;
    private void UpdateHeatResistance(float value) => _heatResistance = value;
}
}