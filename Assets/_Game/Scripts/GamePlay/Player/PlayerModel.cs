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

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerModel: IDisposable
{
    public PlayerStats Stats { get; private set; }
    
    private readonly List<IDisposable> _modules = new();
    public VisionModule Vision { get; private set; }
    public MovementModule Movement { get; private set; }
    public HealthModule Health { get; private set; }
    public EatModule EatModule { get; private set; }
    public AttackModule Attack { get; private set; }
    public EnduranceModule Endurance { get; private set; }
    public DefenseModule Defense { get; private set; }
    public TemperatureModule Temperature { get; private set; }
    
    public AbilitiesModule Abilities { get; private set; }
    public ExperienceModule Experience { get; private set; }
    public EvolutionsModule Evolutions { get; private set; }

    public PlayerModel(PlayerConfig config)
    {
        Stats = new();
        AddModules(config);
        Stats.Initialize(config.InitialConfig);
    }

    private void AddModules(PlayerConfig config)
    {
        Vision = AddModule(new VisionModule(Stats));
        Movement = AddModule(new MovementModule(Stats));
        Health = AddModule(new HealthModule(Stats));
        EatModule = AddModule(new EatModule(Stats));
        Attack = AddModule(new AttackModule(Stats));
        Endurance = AddModule(new EnduranceModule(Stats));
        Defense = AddModule(new DefenseModule(Stats));
        Experience = AddModule(new ExperienceModule(config.ExperienceConfig, EatModule));
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