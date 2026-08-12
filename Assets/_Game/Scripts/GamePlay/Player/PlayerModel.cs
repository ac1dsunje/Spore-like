using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Abilities;
using _Game.Scripts.GamePlay.Player.Modules.Attack;
using _Game.Scripts.GamePlay.Player.Modules.Defense;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Player.Modules.Temperature;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerModel: IDisposable
{
    public PlayerStats Stats { get; private set; }
    public VisionModule Vision { get; private set; }
    public HealthModule Health { get; private set; }
    public DefenseModule Defense { get; private set; }
    public EnduranceModule Endurance { get; private set; }
    public MouthModule MouthModule { get; private set; }
    public AttackModule Attack { get; private set; }
    public MovementModule Movement { get; private set; }
    
    private readonly List<IDisposable> _modules = new();
    public TemperatureModule Temperature { get; private set; }
    public AbilitiesModule Abilities { get; private set; }
    public ExperienceModule Experience { get; private set; }
    public EvolutionsModule Evolutions { get; private set; }

    [Inject]
    public PlayerModel(PlayerConfig config, PlayerStats stats, VisionModule vision, HealthModule health, 
        DefenseModule defense, EnduranceModule endurance, MouthModule mouth, AttackModule attack, MovementModule movement)
    {
        Stats = stats;
        Vision = vision;
        Health = health;
        Defense = defense;
        Endurance = endurance;
        MouthModule = mouth;
        Attack = attack;
        Movement = movement;
        AddModules(config);
        Stats.Initialize(config.InitialConfigs);
    }

    private void AddModules(PlayerConfig config)
    {
        Experience = AddModule(new ExperienceModule(config.ExperienceConfig, MouthModule));
        Abilities = AddModule(new AbilitiesModule());
        Evolutions = AddModule(new EvolutionsModule(this));
        Temperature = AddModule(new TemperatureModule(Stats));
        
    }
    
    private T AddModule<T>(T module) where T : IDisposable
    {
        _modules.Add(module);
        return module;
    }

    public void Dispose()
    {
        foreach (var module in _modules)
        {
            module.Dispose();
        }
    }
}
}