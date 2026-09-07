using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class EnvironmentModule: StatModule
{
    public float PassAbility { get; private set; }
    public float OxygenBreathing { get; private set; }
    public float HydrogenBreathing { get; private set; }
    public float MinimalComfortable => _minimalComfortable - _coldResistance;
    public float MaximumComfortable => _maximumComfortable + _heatResistance;
    
    private float _minimalComfortable;
    private float _maximumComfortable;
    private float _coldResistance;
    private float _heatResistance;

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

    private void UpdatePassability(float value) => PassAbility = value;
    private void UpdateOxygen(float value) => OxygenBreathing = value;
    private void UpdateHydrogen(float value) => HydrogenBreathing = value;
    private void UpdateMinimalComfortable(float value) => _minimalComfortable = value;
    private void UpdateMaximumComfortable(float value) => _maximumComfortable = value;
    private void UpdateColdResistance(float value) => _coldResistance = value;
    private void UpdateHeatResistance(float value) => _heatResistance = value;
}
}