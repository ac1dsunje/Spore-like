using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player.Behaviours;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: EntityScope
{
    
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        
        builder.RegisterComponent(GetComponentInChildren<RigidbodyController>());
        
        builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped).AsSelf();
        builder.RegisterEntryPoint<CombatBinder>(Lifetime.Scoped);
        builder.RegisterEntryPoint<PlayerInput>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<PlayerVision>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponentInChildren<PlayerHealth>())
            .AsSelf()
            .As<IDamageReceiverController>();
        
        builder.RegisterComponent(GetComponentInChildren<PlayerAttack>())
            .AsSelf()
            .As<IDamageSource>()
            .As<IDamageSourceController>()
            .As<IAttackController>();
        
        builder.RegisterComponent(GetComponentInChildren<PlayerMouth>());
        
        builder.RegisterEntryPoint<EntityBasicMovement>(Lifetime.Scoped).As<IMovementController>();
    }
}
}