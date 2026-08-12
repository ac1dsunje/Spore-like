using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Attack;
using _Game.Scripts.GamePlay.Player.Modules.BiomeChecker;
using _Game.Scripts.GamePlay.Player.Modules.Defense;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Player.Modules.Temperature;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: LifetimeScope
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private AnimationConfig _animationConfig;
    [SerializeField] private ExperienceConfig _experienceConfig;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerConfig);
        builder.RegisterInstance(_animationConfig);
        
        builder.RegisterComponentInHierarchy<PlayerController>();
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<PlayerStats>(Lifetime.Scoped);
        
        // Experience
        builder.Register<ExperienceModule>(Lifetime.Scoped);
        builder.RegisterInstance(_experienceConfig);
        
        // Vision
        builder.RegisterComponentInHierarchy<PlayerVision>();
        builder.Register<VisionModule>(Lifetime.Scoped);
        
        // Health
        builder.RegisterComponentInHierarchy<PlayerHealth>();
        builder.Register<HealthModule>(Lifetime.Scoped);
        
        // Defense 
        builder.Register<DefenseModule>(Lifetime.Scoped);
        
        // Endurance
        builder.RegisterComponentInHierarchy<PlayerEndurance>();
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        // Mouth
        builder.RegisterComponentInHierarchy<PlayerMouth>();
        builder.Register<MouthModule>(Lifetime.Scoped);
        
        // Attack
        builder.RegisterComponentInHierarchy<PlayerAttack>();
        builder.Register<AttackModule>(Lifetime.Scoped);
        
        // Movement
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        // Biomes
        builder.RegisterComponentInHierarchy<PlayerBiome>();
        builder.Register<TemperatureModule>(Lifetime.Scoped);
    }
}
}