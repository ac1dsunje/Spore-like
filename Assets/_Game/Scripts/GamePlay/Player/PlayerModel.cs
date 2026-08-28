using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerModel
{
    public EntityStats Stats { get; private set; }
    public VisionModule Vision { get; private set; }
    public HealthModule Health { get; private set; }
    public DefenseModule Defense { get; private set; }
    public EnduranceModule Endurance { get; private set; }
    public MouthModule MouthModule { get; private set; }
    public StomachModule Stomach { get; private set; }
    public AttackModule Attack { get; private set; }
    public MovementModule Movement { get; private set; }
    public TemperatureModule Temperature { get; private set; }
    public ExperienceModule Experience { get; private set; }
    public AbilitiesModule Abilities { get; private set; }
    public EvolutionsModule Evolutions { get; private set; }
    public DisguiseModule Disguise { get; private set; }
    public BiomeModule Biome { get; private set; }
    public BreathingModule Breathing { get; private set; }
    public BuffsModule Buffs { get; private set; }

    [Inject]
    public PlayerModel(EntityStatsConfig config, EntityStats stats, VisionModule vision, HealthModule health, 
        DefenseModule defense, EnduranceModule endurance, MouthModule mouth, AttackModule attack, MovementModule movement,
        TemperatureModule temperature, ExperienceModule experience, AbilitiesModule abilities, EvolutionsModule evolutions,
        DisguiseModule disguise, BiomeModule biome, BreathingModule breathing, BuffsModule buffs, StomachModule stomach)
    {
        Stats = stats;
        Vision = vision;
        Health = health;
        Defense = defense;
        Endurance = endurance;
        MouthModule = mouth;
        Attack = attack;
        Movement = movement;
        Temperature = temperature;
        Experience = experience;
        Abilities = abilities;
        Evolutions = evolutions;
        Disguise = disguise;
        Biome = biome;
        Breathing = breathing;
        Buffs = buffs;
        Stomach = stomach;
        
        Stats.Initialize(config.InitialConfigs);
    }
    
    public void Initialize()
    {
        Abilities.SetModel(this);
        Evolutions.SetModel(this);
        Buffs.SetModel(this);
    }
}
}