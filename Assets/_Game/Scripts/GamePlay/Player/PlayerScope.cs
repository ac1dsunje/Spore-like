using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player.Behaviours;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Rarities;
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
    
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_playerExperienceConfig);
        builder.RegisterInstance(_evolutionsDatabase);
        builder.RegisterInstance(_raritiesDatabase);
        
        builder.RegisterEntryPoint<PlayerController>();
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<EntityStats>(Lifetime.Scoped);
        builder.Register<CombatBinder>(Lifetime.Scoped);
        
        // Experience
        builder.Register<ExperienceModule>(Lifetime.Scoped);
        
        // Vision
        builder.RegisterEntryPoint<PlayerVision>();
        builder.RegisterComponent(GetComponentInChildren<EntityVisionHitbox>());
        builder.Register<VisionModule>(Lifetime.Scoped);
        builder.RegisterComponent(GetComponentInChildren<EntityLighting>());
        
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
        builder.RegisterEntryPoint<EntityEndurance>();
        builder.Register<EnduranceModule>(Lifetime.Scoped);
        
        // Eating
        builder.RegisterComponent(GetComponentInChildren<PlayerMouth>());
        builder.Register<MouthModule>(Lifetime.Scoped);
        builder.Register<StomachModule>(Lifetime.Scoped);
        
        // Movement
        builder.RegisterEntryPoint<PlayerMovement>();
        builder.RegisterComponent(GetComponentInChildren<RigidbodyController>());
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
        builder.Register<EntityBuffsModule>(Lifetime.Scoped);
        
        // Disguise
        builder.Register<DisguiseModule>(Lifetime.Scoped);
        
        // Animation
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
    }
}
}