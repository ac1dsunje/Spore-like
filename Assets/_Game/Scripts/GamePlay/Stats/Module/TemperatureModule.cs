namespace _Game.Scripts.GamePlay.Module
{
public class TemperatureModule: StatModule
{
    public float MinimalLethal => _minimalLethal + _coldResistance;
    public float MaximumLethal => _maximumLethal + _heatResistance;
    public float MinimalComfortable => _minimalComfortable + _coldResistance;
    public float MaximumComfortable => _maximumComfortable + _heatResistance;
    
    public bool IsLethal(float value) => value < MinimalLethal || value > MaximumLethal;
    public bool IsUncomfortable(float value) => value < MinimalComfortable || value > MaximumComfortable;

    private float _minimalLethal;
    private float _maximumLethal;
    private float _minimalComfortable;
    private float _maximumComfortable;
    private float _coldResistance;
    private float _heatResistance;

    protected override void Configure()
    {
        BindStat(StatType.MinimalLethalTemperature, UpdateMinimalLethal);
        BindStat(StatType.MaximumLethalTemperature, UpdateMaximumLethal);
        BindStat(StatType.MinimalComfortableTemperature, UpdateMinimalComfortable);
        BindStat(StatType.MaximumComfortableTemperature, UpdateMaximumComfortable);
        BindStat(StatType.ColdResistance, UpdateColdResistance);
        BindStat(StatType.HeatResistance, UpdateHeatResistance);
    }

    private void UpdateMinimalLethal(float value) => _minimalLethal = value;
    private void UpdateMaximumLethal(float value) => _maximumLethal = value;
    private void UpdateMinimalComfortable(float value) => _minimalComfortable = value;
    private void UpdateMaximumComfortable(float value) => _maximumComfortable = value;
    private void UpdateColdResistance(float value) => _coldResistance = value;
    private void UpdateHeatResistance(float value) => _heatResistance = value;
    
}
}