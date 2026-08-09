using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Temperature
{
public class TemperatureModule: StatModule
{
    public bool IsLethal(float value) => value < GetMinimalLethal() || value > GetMaximumLethal();
    public bool IsComfortable(float value) => value >= GetMinimalComfortable() || value <= GetMaximumComfortable();

    private float _minimalLethal;
    private float _maximumLethal;
    private float _minimalComfortable;
    private float _maximumComfortable;
    private float _coldResistance;
    private float _heatResistance;

    public TemperatureModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.MinimalLethalTemperature, UpdateMinimalLethal);
        BindStat(StatType.MinimalComfortableTemperature, UpdateMinimalComfortable);
        BindStat(StatType.MaximumComfortableTemperature, UpdateMaximumComfortable);
        BindStat(StatType.MaximumLethalTemperature, UpdateMaximumLethal);
        BindStat(StatType.ColdResistance, UpdateColdResistance);
        BindStat(StatType.HeatResistance, UpdateHeatResistance);
    }

    private void UpdateMinimalLethal(float value) => _minimalLethal = value;
    private void UpdateMaximumLethal(float value) => _maximumLethal = value;
    private void UpdateMinimalComfortable(float value) => _minimalComfortable = value;
    private void UpdateMaximumComfortable(float value) => _maximumComfortable = value;
    private void UpdateColdResistance(float value) => _coldResistance = value;
    private void UpdateHeatResistance(float value) => _heatResistance = value;
    
    
    private float GetMinimalLethal() => _minimalLethal + _coldResistance;
    private float GetMaximumLethal() => _maximumLethal + _heatResistance;
    private float GetMinimalComfortable() => _minimalComfortable + _coldResistance;
    private float GetMaximumComfortable() => _maximumComfortable + _heatResistance;
    
}
}