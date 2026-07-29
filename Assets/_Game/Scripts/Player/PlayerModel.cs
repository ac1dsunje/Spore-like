using System;
using System.Collections.Generic;
using _Game.Scripts.Player.Modules;
using _Game.Scripts.Player.Modules.Attack;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Mouth;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.Player.Modules.Vision;

namespace _Game.Scripts.Player
{
public class PlayerModel: IDisposable
{
    public VisionModule Vision { get; private set; }
    public MovementModule Movement { get; private set; }
    public HealthModule Health { get; private set; }
    public EatModule EatModule { get; private set; }
    public AttackModule Attack { get; private set; }
    
    private readonly List<StatModule> _modules = new();
    public ExperienceController Experience { get; }
    public PlayerStatsModule Stats { get; private set; }

    public PlayerModel(PlayerConfig config)
    {
        Stats = new();
        AddModules();
        Stats.Initialize(config.InitialConfig);
        Experience = new(config.ExperienceConfig, EatModule);
    }

    private void AddModules()
    {
        Vision = new(Stats);
        Movement = new(Stats);
        Health = new(Stats);
        EatModule = new (Stats);
        Attack = new(Stats);

        _modules.Add(Vision);
        _modules.Add(Movement);
        _modules.Add(Health);
        _modules.Add(EatModule);
        _modules.Add(Attack);
    }

    public void Dispose()
    {
        foreach (var module in _modules)
        {
            module.Dispose();
        }
        Experience.Dispose();
    }
}
}