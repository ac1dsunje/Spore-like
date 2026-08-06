using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Player.Modules.Abilities;
using _Game.Scripts.GamePlay.Player.Modules.Attack;
using _Game.Scripts.GamePlay.Player.Modules.Defense;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
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
    
    public AbilitiesModule Abilities { get; private set; }
    public ExperienceController Experience { get; }

    public PlayerModel(PlayerConfig config)
    {
        Stats = new();
        AddModules();
        Stats.Initialize(config.InitialConfig);
        Experience = new(config.ExperienceConfig, EatModule);
        Abilities = new();
        _modules.Add(Experience);
    }

    private void AddModules()
    {
        Vision = new(Stats);
        Movement = new(Stats);
        Health = new(Stats);
        EatModule = new (Stats);
        Attack = new(Stats);
        Endurance = new(Stats);
        Defense = new(Stats);

        _modules.Add(Vision);
        _modules.Add(Movement);
        _modules.Add(Health);
        _modules.Add(EatModule);
        _modules.Add(Attack);
        _modules.Add(Endurance);
        _modules.Add(Defense);
    }

    public void Dispose()
    {
        foreach (var module in _modules)
        {
            module.Dispose();
        }
        Experience.Dispose();
        Abilities.Dispose();
    }
}
}