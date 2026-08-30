using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
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
    [SerializeField] private AnimationSettings _animationSettings;
    [SerializeField] private PlayerExperienceConfig _playerExperienceConfig;
    
    [SerializeField] private BuffsDatabase _buffsDatabase;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_playerExperienceConfig);
        
        builder.RegisterInstance(_buffsDatabase);
        
        builder.Register<PlayerInputService>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponent<PlayerController>());
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<EntityStats>(Lifetime.Scoped);
        builder.Register<CombatBinder>(Lifetime.Scoped);
        
        // Experience
        builder.Register<ExperienceModule>(Lifetime.Scoped);
        
        // Vision
        builder.RegisterComponent(GetComponentInChildren<PlayerVision>());
        builder.Register<VisionModule>(Lifetime.Scoped);
        builder.RegisterComponent(GetComponentInChildren<PlayerXRay>());
        
        // Combat
        builder.RegisterComponent(GetComponentInChildren<PlayerHealth>())
            .AsSelf()
            .As<IDamageReceiver>()
            .As<IDamageReceiverController>();
        builder.Register<HealthModule>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponentInChildren<PlayerAttack>())
            .AsSelf()
            .As<IDamageSource>()
            .As<IDamageSourceController>();
        builder.Register<AttackModule>(Lifetime.Scoped);
        
        // Defense 
        builder.Register<DefenseModule>(Lifetime.Scoped);
        
        // Endurance
        builder.RegisterComponent(GetComponentInChildren<PlayerEndurance>());
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        // Eating
        builder.RegisterComponent(GetComponentInChildren<PlayerMouth>());
        builder.Register<MouthModule>(Lifetime.Scoped);
        builder.Register<StomachModule>(Lifetime.Scoped);
        
        // Movement
        builder.RegisterEntryPoint<PlayerMovement>();
        builder.RegisterComponent(GetComponentInChildren<MovementController>());
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        // Biomes
        builder.RegisterEntryPoint<EntityBiomeChecker>(Lifetime.Scoped);
        builder.Register<BiomeModule>(Lifetime.Scoped);
        
        builder.Register<BreathingModule>(Lifetime.Scoped);
        builder.Register<TemperatureModule>(Lifetime.Scoped);
        
        // Abilities
        builder.Register<AbilitiesModule>(Lifetime.Scoped);
        
        // Evolutions
        builder.Register<EvolutionsModule>(Lifetime.Scoped);
        
        // Buffs
        builder.Register<BuffsModule>(Lifetime.Scoped);
        
        // Disguise
        builder.RegisterComponent(GetComponentInChildren<PlayerDisguise>());
        builder.Register<DisguiseModule>(Lifetime.Scoped);
        
        // Animation
        builder.RegisterComponent(GetComponentInChildren<ItemAnimation>());
    }
}
}