using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Sensorics
{
public class SensoricsModule: StatModule
{
    public float Sensorics { get; private set; }

    [Inject]
    public SensoricsModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.Sensorics, UpdateSensorics);
    }
    
    private void UpdateSensorics(float value) => Sensorics = value;
}
}