using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Defense;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: LifetimeScope
{
    [SerializeField] private PlayerConfig _playerConfig;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerConfig);
        
        builder.RegisterComponentInHierarchy<PlayerController>();
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<PlayerStats>(Lifetime.Scoped);
        
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
    }
}
}