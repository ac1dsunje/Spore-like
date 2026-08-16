using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Network;
using _Game.Scripts.GamePlay.Player.Behaviours;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: LifetimeScope
{
    [SerializeField] private EntityStatsConfig _entityStatsConfig;
    [SerializeField] private AnimationConfig _animationConfig;
    [SerializeField] private PlayerExperienceConfig _playerExperienceConfig;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationConfig);
        builder.RegisterInstance(_playerExperienceConfig);
        
        builder.Register<PlayerInputService>(Lifetime.Scoped);
        builder.Register<EntityAuthority>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponent<PlayerController>());
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<EntityStats>(Lifetime.Scoped);
        
        // Experience
        builder.Register<ExperienceModule>(Lifetime.Scoped);
        
        // Vision
        builder.RegisterComponent(GetComponentInChildren<PlayerVision>());
        builder.Register<VisionModule>(Lifetime.Scoped);
        
        // Health
        builder.RegisterComponent(GetComponentInChildren<PlayerHealth>());
        builder.Register<HealthModule>(Lifetime.Scoped);
        
        // Defense 
        builder.Register<DefenseModule>(Lifetime.Scoped);
        
        // Endurance
        builder.RegisterComponent(GetComponentInChildren<PlayerEndurance>());
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        // Mouth
        builder.RegisterComponent(GetComponentInChildren<PlayerMouth>());
        builder.Register<MouthModule>(Lifetime.Scoped);
        
        // Attack
        builder.RegisterComponent(GetComponentInChildren<PlayerAttack>());
        builder.Register<AttackModule>(Lifetime.Scoped);
        
        // Movement
        builder.RegisterComponent(GetComponentInChildren<PlayerMovement>());
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        // Biomes
        builder.RegisterComponent(GetComponentInChildren<PlayerBiome>());
        builder.Register<BiomeModule>(Lifetime.Scoped);
        builder.Register<TemperatureModule>(Lifetime.Scoped);
        
        // Abilities
        builder.Register<AbilitiesModule>(Lifetime.Scoped);
        
        // Evolutions
        builder.Register<EvolutionsModule>(Lifetime.Scoped);
        
        // Disguise
        builder.Register<DisguiseModule>(Lifetime.Scoped);
        
        // Animation
        builder.RegisterComponent(GetComponentInChildren<ItemAnimation>());
    }
}
}