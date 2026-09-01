using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player.Behaviours;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: EntityScope
{
    [SerializeField] private EntityStatsConfig _entityStatsConfig;
    [SerializeField] private AnimationSettings _animationSettings;
    [SerializeField] private PlayerExperienceConfig _playerExperienceConfig;
    
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationSettings);
        builder.RegisterInstance(_playerExperienceConfig);
        builder.RegisterInstance(_evolutionsDatabase);
        builder.RegisterInstance(_raritiesDatabase);
        
        builder.RegisterComponent(GetComponentInChildren<RigidbodyController>());
        builder.RegisterComponent(GetComponentInChildren<EntityVisionHitbox>());
        builder.RegisterEntryPoint<EntityBiomeChecker>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped);
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<CombatBinder>(Lifetime.Scoped);
        builder.RegisterEntryPoint<PlayerInput>(Lifetime.Scoped);
        
        builder.Register<ExperienceModule>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<PlayerVision>(Lifetime.Scoped);
        builder.RegisterComponent(GetComponentInChildren<EntityLighting>());
        
        builder.RegisterComponent(GetComponentInChildren<PlayerHealth>())
            .AsSelf()
            .As<IDamageReceiver>()
            .As<IDamageReceiverController>();
        
        builder.RegisterComponent(GetComponentInChildren<PlayerAttack>())
            .AsSelf()
            .As<IDamageSource>()
            .As<IDamageSourceController>()
            .As<IAttackController>();
        
        builder.RegisterComponent(GetComponentInChildren<PlayerMouth>());
        
        builder.RegisterEntryPoint<EntityBasicMovement>(Lifetime.Scoped).As<IMovementController>();
        
        builder.Register<AbilitiesModule>(Lifetime.Scoped);
        
        builder.Register<EvolutionsModule>(Lifetime.Scoped);
        
        builder.Register<EntityBuffsModule>(Lifetime.Scoped);
    }
}
}