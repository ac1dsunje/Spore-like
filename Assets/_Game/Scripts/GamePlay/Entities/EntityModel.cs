using _Game.Scripts.GamePlay.Entities.Experience;
using _Game.Scripts.GamePlay.Modules;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityModel
{
    [Inject] public EntityStats Stats { get; private set; }
    [Inject] public VisionModule Vision { get; private set; }
    [Inject] public HealthModule Health { get; private set; }
    [Inject] public DefenseModule Defense { get; private set; }
    [Inject] public EnduranceModule Endurance { get; private set; }
    [Inject] public PickingModule Picking { get; private set; }
    [Inject] public StomachModule Stomach { get; private set; }
    [Inject] public AttackModule Attack { get; private set; }
    [Inject] public MovementModule Movement { get; private set; }
    [Inject] public TemperatureModule Temperature { get; private set; }
    [Inject] public DisguiseModule Disguise { get; private set; }
    [Inject] public BiomeModule Biome { get; private set; }
    [Inject] public BreathingModule Breathing { get; private set; }
}
}